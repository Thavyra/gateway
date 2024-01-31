using Gateway.Configuration.Services;
using Yarp.ReverseProxy.Configuration;

namespace Gateway.Configuration.Routes;

public class RouteOptions
{
    public required string Name { get; set; }
    public required MatchOptions Match { get; set; }
    public TransformOptions? Transforms { get; set; }

    
    public RouteConfig Build(ServiceOptions service)
    {
        return new RouteConfig
        {
            RouteId = Name,
            ClusterId = service.Name,
            AuthorizationPolicy = service.Authorization is not null ? $"{service.Name}-service-policy" : null,
            Match = Match.Build(),
            Transforms = Transforms?.Build()
        };
    }
}