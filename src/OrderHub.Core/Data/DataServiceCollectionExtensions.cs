using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderHub.Core.Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddOrderHubData(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Falta ConnectionStrings:Default");

        services.AddDbContext<OrderHubDbContext>(options =>
            options.UseSqlite(SqlitePath.Normalize(connectionString)));

        return services;
    }
}
