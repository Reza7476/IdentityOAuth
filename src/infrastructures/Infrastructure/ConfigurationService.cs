// در لایه Infrastructure
using Microsoft.Extensions.Configuration;

public class ConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }
}
