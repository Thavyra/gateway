using Gateway.Configuration.Authentication;
using Gateway.Configuration.Services;
using Yarp.ReverseProxy.Configuration;

namespace Gateway.Configuration;

public class GatewayConfiguration
{
    public Dictionary<string, SchemeOptions> Schemes { get; set; } = [];
    public Dictionary<string, ServiceOptions> Services { get; set; } = [];

    public IEnumerable<(ClusterConfig Cluster, IReadOnlyList<RouteConfig> Routes)> BuildServices()
    {
        foreach (var (name, service) in Services)
        {
            yield return service.Build(name);
        }
    }
}