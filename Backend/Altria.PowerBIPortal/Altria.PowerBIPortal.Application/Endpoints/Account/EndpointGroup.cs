using Altria.PowerBIPortal.Application.Infrastructure;

namespace Altria.PowerBIPortal.Application.Endpoints.Account;

public class EndpointGroup : IEndpointGroup
{
    public IEndpointRouteBuilder Configure(IEndpointRouteBuilder app)
    {
        return app
            .MapGroup("/account")
            .WithTags("Account")
            .WithOpenApi();
    }
}
