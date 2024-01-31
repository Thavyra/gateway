using Microsoft.AspNetCore.Authentication;

namespace Gateway.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public string? Key { get; set; }
}