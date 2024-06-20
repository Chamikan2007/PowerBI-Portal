using Altria.PowerBIPortal.Domain.Contracts.DomainServices;

namespace Altria.PowerBIPortal.Application.BackgroundServices;

public class SubscripionsProcessor : IHostedService, IDisposable
{
    private Timer? _timer = null;
    private int _subscriptionsBeingProcessed = 0; // 0 for false, 1 for true.
    private readonly IServiceProvider _services;

    public SubscripionsProcessor(IServiceProvider services)
    {
        _services = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(ProcessSubscriptionsAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void ProcessSubscriptionsAsync(object? state)
    {
        // Only start processing the subscriptions iif the subscriptions are not being proccessed already.
        if(Interlocked.Exchange(ref _subscriptionsBeingProcessed, 1) == 1)
        {
            return;
        }

        using var scope = _services.CreateScope();
        var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
        await subscriptionService.ProcessSubscriptionsAsync();

        Interlocked.Exchange(ref _subscriptionsBeingProcessed, 0);
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
