using Gateway.Authentication;
using Gateway.Configuration.Services;
using AuthorizationOptions = Microsoft.AspNetCore.Authorization.AuthorizationOptions;

namespace Gateway.Authorization;

public static class Extensions
{
    public static IServiceCollection AddGatewayAuthorization(this IServiceCollection services,
        IDictionary<string, ServiceOptions> serviceConfig)
    {
        services.AddAuthorization(options =>
        {
            foreach (var (name, service) in serviceConfig)
            {
                options.AddServicePolicy(name, service);
            }
        });
        
        return services;
    }

    private static void AddServicePolicy(this AuthorizationOptions options, string name, ServiceOptions service)
    {
        if (service.Authorization is null)
        {
            return;
        }
        
        options.AddPolicy($"{name}-service-policy", builder =>
        {
            builder.RequireAuthenticatedUser();

            if (service.Authorization.Schemes.Count > 0)
            {
                builder.AddAuthenticationSchemes(service.Authorization.Schemes.ToArray());
            }
        });
    }
}