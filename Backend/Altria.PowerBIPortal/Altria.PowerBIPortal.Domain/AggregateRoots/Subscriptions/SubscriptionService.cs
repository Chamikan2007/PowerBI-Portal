using Altria.PowerBIPortal.Domain.Contracts;
using Altria.PowerBIPortal.Domain.Contracts.DomainServices;
using Altria.PowerBIPortal.Domain.Contracts.IPowerBIService;
using Altria.PowerBIPortal.Domain.Contracts.Repositories;

namespace Altria.PowerBIPortal.Domain.AggregateRoots.Subscriptions;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRequestRepository _subscriptionRepository;
    private readonly IPowerBIReportService _powerBIReportService;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionService(ISubscriptionRequestRepository subscriptionRepository, IPowerBIReportService powerBIReportService, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _powerBIReportService = powerBIReportService;
        _unitOfWork = unitOfWork;
    }

    public async Task ProcessSubscriptionsAsync()
    {
        var subscriptions = await _subscriptionRepository.FetchSubscriptionsToProcessAsync();

        foreach (var subscription in subscriptions)
        {
            var subscritionId = await _powerBIReportService.ProcessSubscriptionAsync(subscription);
            subscription.Processed(subscritionId);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
