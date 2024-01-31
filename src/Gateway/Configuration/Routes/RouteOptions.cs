using Gateway.Configuration.Services;
using Yarp.ReverseProxy.Configuration;

namespace Gateway.Configuration.Routes;

public class RouteOptions
{
    public required string Name { get; set; }
    public required MatchOptions Match { get; set; }
    public TransformOptions? Transforms { get; set; }

    
    public RouteConfig Build(ServiceOptions service, string serviceName)
    {
        return new RouteConfig
        {
            RouteId = Name,
            ClusterId = serviceName,
            AuthorizationPolicy = service.Authorization is not null ? $"{serviceName}-service-policy" : "anonymous",
            Match = Match.Build(),
            Transforms = Transforms?.Build()
        };
    }
}