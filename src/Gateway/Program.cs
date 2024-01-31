using Gateway.Authentication;
using Gateway.Authorization;
using Gateway.Configuration;
using Gateway.Configuration.Services;

var builder = WebApplication.CreateBuilder(args);

// Import configuration files

builder.Configuration.AddYamlFile("gateway.yml", optional: true);

// Parse configuration

var gatewayConfiguration = builder.Configuration.Get<GatewayConfiguration>();

if (gatewayConfiguration is null)
{
    throw new Exception("No Gateway configuration provided.");
}

// Add auth schemes/service policies

builder.Services
    .AddGatewayAuthentication(gatewayConfiguration.Schemes)
    .AddGatewayAuthorization(gatewayConfiguration.Services);

// Generate routes and clusters for reverse proxy

var serviceClusters = gatewayConfiguration.BuildServices().ToList();

var routes = serviceClusters.SelectMany(x => x.Routes).ToList();
var clusters = serviceClusters.Select(x => x.Cluster).ToList();

// Add reverse proxy

builder.Services.AddReverseProxy()
    .LoadFromMemory(routes, clusters);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();