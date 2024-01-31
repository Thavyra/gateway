using Gateway.Configuration.Consumers;
using Gateway.Configuration.Services;

namespace Gateway.Configuration;

public class Configuration
{
    public List<ConsumerOptions> Consumers { get; set; } = [];
    public List<ServiceOptions> Services { get; set; } = [];
}