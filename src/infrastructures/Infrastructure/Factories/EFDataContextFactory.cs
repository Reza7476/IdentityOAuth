using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Factories;
public class EFDataContextFactory : IDesignTimeDbContextFactory<EFDataContext>
{
    public EFDataContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) //
            .AddJsonFile("InfraAppSetting.json", optional: false, reloadOnChange: true)
            .Build();

        // دریافت ConnectionString از InfraAppSetting.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("ConnectionString not found in InfraAppSetting.json" + basePath);
        }


        var optionBuilder = new DbContextOptionsBuilder<EFDataContext>();
        optionBuilder.UseSqlServer(connectionString);

        return new EFDataContext(optionBuilder.Options);
    }
}