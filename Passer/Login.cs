using System.Collections.Generic;

namespace Passer;

public class Login
{
    public string? WebsiteUrl { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public List<string>? Tags { get; set; } = [];
    
    public override string ToString()
    {
        return $"Website URL: {WebsiteUrl}\nUsername: {Username}\nPassword: {Password}\nTags: {string.Join(", ", Tags ?? [])}";
    }
    
}