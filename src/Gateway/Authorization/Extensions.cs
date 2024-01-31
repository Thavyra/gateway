using Gateway.Authentication;
using Gateway.Configuration.Services;
using AuthorizationOptions = Microsoft.AspNetCore.Authorization.AuthorizationOptions;

namespace Gateway.Authorization;

public static class Extensions
{
    public static IServiceCollection AddGatewayAuthorization(this IServiceCollection services,
        Configuration.Configuration configuration)
    {
        services.AddAuthorization(options =>
        {
            foreach (var service in configuration.Services)
            {
                options.AddServicePolicy(service);
            }
        });
        
        return services;
    }

    private static void AddServicePolicy(this AuthorizationOptions options, ServiceOptions service)
    {
        if (service.Authorization is null)
        {
            return;
        }
        
        options.AddPolicy($"{service.Name}-service-policy", builder => _ = service.Authorization switch
        {
            { Consumers.Count: > 0 } => builder
                .RequireClaim(Claims.Consumer, service.Authorization.Consumers)
                .AddAuthenticationSchemes(service.Authorization.Consumers.ToArray()),
            
            not null => builder.RequireAuthenticatedUser(),
            
            _ => builder.RequireAssertion(_ => true)
        });
    }
}