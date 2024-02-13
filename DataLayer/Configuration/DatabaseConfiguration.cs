using Microsoft.Extensions.Configuration;

namespace DataLayer.Configuration;

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