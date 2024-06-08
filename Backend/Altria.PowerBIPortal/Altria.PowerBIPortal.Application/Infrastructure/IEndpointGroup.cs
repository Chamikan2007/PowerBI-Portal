namespace Altria.PowerBIPortal.Application.Infrastructure;

public interface IEndpointGroup
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder app);
}
