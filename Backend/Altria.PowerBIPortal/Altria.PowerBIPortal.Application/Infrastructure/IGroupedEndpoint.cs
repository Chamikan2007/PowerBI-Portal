namespace Altria.PowerBIPortal.Application.Infrastructure;

public interface IGroupedEndpoint
{
    public void Configure(IEndpointRouteBuilder app);
}

public interface IGroupedEndpoint<TEndpointGroup> : IGroupedEndpoint where TEndpointGroup : class, IEndpointGroup
{
}