using FreeCourse.IdentityServer;
using FreeCourse.IdentityServer.Data;
using FreeCourse.IdentityServer.Models;
using FreeCourse.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");


try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    //builder.Services.AddIdentityServer().AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddLocalApiAuthentication();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    using(var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!userManager.Users.Any())
        {
            userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Admin",
                Email = "mrorhanfirtina@gmail.com",
                City = "Istanbul"
            },"Mineefe-1").Wait();
        }
    }

    app.Run();
}
catch (Exception ex) when (
                            // https://github.com/dotnet/runtime/issues/60600
                            ex.GetType().Name is not "StopTheHostException"
                            // HostAbortedException was added in .NET 7, but since we target .NET 6 we
                            // need to do it this way until we target .NET 8
                            && ex.GetType().Name is not "HostAbortedException"
                        )
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}