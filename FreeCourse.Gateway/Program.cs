using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var ocelotConfig = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json", optional: true) // Ortama özgü yapýlandýrma dosyasýný ekleyin
    .Build();



builder.Services.AddOcelot(ocelotConfig);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
