﻿using Altria.PowerBIPortal.Application.Infrastructure;

namespace Altria.PowerBIPortal.Application.Endpoints.Users.GetUsers;

public class Endpoint : IGroupedEndpoint<EndpointGroup>
{
    public void Configure(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async () =>
        {
            var users = new List<UserModel>
            {
                new UserModel
                {
                    Id = 1,
                    Name = "Stephanie E. Bardsley",
                    Email = "jado_maleyu8@aol.com",
                },
                new UserModel
                {
                    Id = 2,
                    Name = "Rolando A. Elder",
                    Email = "xekevov_ona92@hotmail.com",
                },
            };
            await Task.CompletedTask;
            return users;
        });
    }
}
