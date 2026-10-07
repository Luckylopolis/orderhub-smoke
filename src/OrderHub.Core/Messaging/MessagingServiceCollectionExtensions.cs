using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderHub.Core.Messaging;

public static class MessagingServiceCollectionExtensions
{
    public static IServiceCollection AddOrderHubMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.Section));
        services.AddSingleton<RabbitMqConnectionProvider>();
        return services;
    }
}
