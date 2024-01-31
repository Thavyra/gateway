using Gateway.Configuration.Transform;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Gateway.Configuration.Routes;

public class TransformOptions
{
    public PathTransformOptions? Path { get; set; }
    public List<HeaderTransformOptions> Headers { get; set; } = [];


    public IReadOnlyList<IReadOnlyDictionary<string, string>>? Build()
    {
        var config = new RouteConfig();
        
        config = Path switch
        {
            { Set: { } transformPathSet } => config.WithTransformPathSet(transformPathSet),
            _ => config
        };

        config = Headers.Aggregate(config, (current, header) => header switch
        {
            { Name: { } headerName, Set: { } headerSet } 
                => current.WithTransformRequestHeader(headerName, headerSet),

            { Remove: { } removeHeaderName }
                => current.WithTransformRequestHeaderRemove(removeHeaderName),

            _ => current
        });

        return config.Transforms;
    }
}