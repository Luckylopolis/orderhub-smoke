using OrderHub.Core.Messaging;

namespace OrderHub.Worker;

public class SmokeWorker(RabbitMqConnectionProvider rabbit, ILogger<SmokeWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await rabbit.GetConnectionAsync(stoppingToken);
                logger.LogInformation("Worker OK: conectado a RabbitMQ.");
                return;
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                logger.LogWarning("RabbitMQ no disponible ({Message}). Reintentando en 5s...", ex.Message);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
