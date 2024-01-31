using Gateway;
using Gateway.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddYamlFile("gateway.yml", optional: true);

var configuration = builder.Configuration.Get<Configuration>();

if (configuration is null)
{
    throw new Exception("No Gateway configuration provided.");
}

builder.Services.AddGateway(configuration);

var app = builder.Build();

app.MapReverseProxy();

app.Run();