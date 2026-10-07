using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderHub.Core.Data;
using OrderHub.Core.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrderHubData(builder.Configuration);
builder.Services.AddOrderHubMessaging(builder.Configuration);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .WithOrigins(builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? [])
    .AllowAnyHeader()
    .AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderHub Smoke", Version = "v1" }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.MapGet("/health", async (OrderHubDbContext db, RabbitMqConnectionProvider rabbit, CancellationToken ct) =>
{
    var database = await Check(async () =>
    {
        await db.Database.OpenConnectionAsync(ct);
        await db.Database.CloseConnectionAsync();
    });
    var rabbitmq = await Check(async () => await rabbit.GetConnectionAsync(ct));

    return Results.Ok(new { api = "ok", database, rabbitmq });
})
.WithName("Health");

app.Run();

static async Task<string> Check(Func<Task> probe)
{
    try
    {
        await probe();
        return "ok";
    }
    catch (Exception ex)
    {
        return $"error: {ex.Message}";
    }
}
