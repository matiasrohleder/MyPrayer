using Microsoft.Extensions.Configuration;

namespace Entities.Helpers;

public class DatabaseConfiguration
{
    public string? ModelProvider { get; set; }
    public string? ModelConnection { get; set; }

    public DatabaseConfiguration Bind(IConfiguration configuration)
    {
        configuration.GetSection("DatabaseConfiguration").Bind(this);
        return this;
    }
}