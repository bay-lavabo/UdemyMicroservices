using FreeCourse.Services.Order.Infrastructure.DbContexts;
using FreeCourse.Shared.Services.Abstract;
using FreeCourse.Shared.Services.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

//DbContext, ConnectionString & Migration ayarlar�.
builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        configure => configure.MigrationsAssembly("FreeCourse.Services.Order.Infrastructure")
    );
});

//MediatR k�t�phanesi ekleme.MediatR kolland���m�z assembly leri tan�ml�yoruz.
builder.Services.AddMediatR(typeof(FreeCourse.Services.Order.Application.Handlers.Commands.Insert.CreateOrderCommandHandler).Assembly);

//Kullan�c� bilgileri i�in ISharedIdentityService i�in tan�mlama yap�yoruz. HttpConnectAccesor'� de eklememiz gerekiyor.
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
