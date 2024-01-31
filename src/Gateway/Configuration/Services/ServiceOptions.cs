using Yarp.ReverseProxy.Configuration;
using RouteOptions = Gateway.Configuration.Routes.RouteOptions;

namespace Gateway.Configuration.Services;

public class ServiceOptions
{
    public string? Url { get; set; }
    public List<RouteOptions> Routes { get; set; } = [];
    public AuthorizationOptions? Authorization { get; set; }

    public (ClusterConfig Cluster, IReadOnlyList<RouteConfig> Routes) Build(string name)
    {
        var cluster = new ClusterConfig
        {
            ClusterId = name,
            Destinations = Url is not null
                ? new Dictionary<string, DestinationConfig> { ["default"] = new() { Address = Url } }
                : new Dictionary<string, DestinationConfig>()
        };

        var routes = Routes.Select(x => x.Build(this, name)).ToList();

        return (cluster, routes);
    }
}