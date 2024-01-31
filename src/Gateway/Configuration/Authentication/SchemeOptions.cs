namespace Gateway.Configuration.Authentication;

public class SchemeOptions
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? RedirectUri { get; set; }

    public string? Key { get; set; }
}