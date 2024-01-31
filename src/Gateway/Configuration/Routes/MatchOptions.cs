using Yarp.ReverseProxy.Configuration;

namespace Gateway.Configuration.Routes;

public class MatchOptions
{
    public string? Path { get; set; }


    public RouteMatch Build()
    {
        return new RouteMatch
        {
            Path = Path
        };
    }
}