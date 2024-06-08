using System.Reflection;

namespace Altria.PowerBIPortal.Application.Infrastructure;

public static class EndpointExtentions
{
    public static IServiceCollection RegisterEndpoints(this IServiceCollection builder)
    {
        #region Register Grouped Endpoints

        var endpointGroups = Assembly.GetEntryAssembly()!
            .GetTypes()
            .Where(t => typeof(IEndpointGroup).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        if (endpointGroups != null)
        {
            foreach (var group in endpointGroups)
            {
                builder.AddSingleton(typeof(IEndpointGroup), group);

                var groupedEndpoints = Assembly.GetEntryAssembly()!
                    .GetTypes()
                    .Where(t => GetGroupedEndpoints(t, group));

                foreach (var groupedEndpoint in groupedEndpoints)
                {
                    var endpointInstance = (IGroupedEndpoint)Activator.CreateInstance(groupedEndpoint)!;
                    builder.AddKeyedSingleton(group.FullName, endpointInstance);
                }
            }
        }

        #endregion

        #region Register Endpoints

        var endpoints = Assembly.GetEntryAssembly()!
            .GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var endpoint in endpoints)
        {
            builder.AddSingleton(typeof(IEndpoint), endpoint);
        }

        #endregion

        return builder;
    }

    public static IEndpointRouteBuilder UseEndpoints(this IEndpointRouteBuilder app)
    {
        #region Use Grouped Endpoints

        var endpointGroups = app.ServiceProvider.GetServices<IEndpointGroup>();

        if (endpointGroups != null)
        {
            foreach (var group in endpointGroups)
            {
                var endpointGroup = group.Configure(app);

                var groupedEndpoints = app.ServiceProvider.GetKeyedServices<IGroupedEndpoint>(group.GetType().FullName);

                foreach (var endpoint in groupedEndpoints)
                {
                    endpoint.Configure(endpointGroup);
                }
            }
        }

        #endregion

        #region Use Endpoints

        var endpoints = app.ServiceProvider.GetServices<IEndpoint>();

        if (endpoints != null)
        {
            foreach (var endpoint in endpoints)
            {
                endpoint.Configure(app);
            }
        }

        #endregion

        return app;
    }

    private static bool GetGroupedEndpoints(Type t, Type group)
    {
        if (t.IsAbstract || t.IsInterface)
        {
            return false;
        }

        var implementedInterfaces = t.GetInterfaces();

        foreach (var implementedInterface in implementedInterfaces)
        {
            if (!implementedInterface.IsGenericType)
            {
                continue;
            }

            if (implementedInterface.GetGenericTypeDefinition() != typeof(IGroupedEndpoint<>).GetGenericTypeDefinition())
            {
                continue;
            }

            if (string.Equals(implementedInterface.GetGenericArguments()[0].FullName, group.FullName))
            {
                return true;
            }
        }

        return false;
    }
}