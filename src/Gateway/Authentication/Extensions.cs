namespace Gateway.Authentication;

public static class Extensions
{
    public static IServiceCollection AddGatewayAuthentication(this IServiceCollection services,
        Configuration.Configuration configuration)
    {
        var builder = services.AddAuthentication();

        foreach (var consumer in configuration.Consumers) switch (consumer)
        {
            case { Key: not null }:
                builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                    authenticationScheme: consumer.Name,
                    options => options.Key = consumer.Key);
                
                break;
        }
        
        return services;
    }
}