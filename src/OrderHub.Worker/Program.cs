using OrderHub.Core.Data;
using OrderHub.Core.Messaging;
using OrderHub.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOrderHubData(builder.Configuration);
builder.Services.AddOrderHubMessaging(builder.Configuration);
builder.Services.AddHostedService<SmokeWorker>();

builder.Build().Run();
