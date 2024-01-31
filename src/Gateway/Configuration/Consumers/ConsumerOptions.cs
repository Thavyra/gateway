namespace Gateway.Configuration.Consumers;

public class ConsumerOptions
{
    public required string Name { get; set; }

    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? RedirectUri { get; set; }

    public string? Key { get; set; }
}