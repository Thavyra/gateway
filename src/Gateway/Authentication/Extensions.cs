using Gateway.Configuration.Authentication;

namespace Gateway.Authentication;

public static class Extensions
{
    public static IServiceCollection AddGatewayAuthentication(this IServiceCollection services,
        IDictionary<string, SchemeOptions> schemes)
    {
        var builder = services.AddAuthentication();

        foreach (var (name, scheme) in schemes) switch (scheme)
        {
            case { Key: not null }:
                builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                    authenticationScheme: name,
                    options => options.Key = scheme.Key);
                
                break;
        }
        
        return services;
    }
}