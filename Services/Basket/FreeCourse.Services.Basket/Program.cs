
using FreeCourse.Services.Basket.Repository.Context;
using FreeCourse.Services.Basket.Services.Abstract;
using FreeCourse.Services.Basket.Services.Concrete;
using FreeCourse.Services.Basket.Settings;
using FreeCourse.Shared.Services.Abstract;
using FreeCourse.Shared.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

// Add services to the container.

builder.Services.AddControllers(opt => 
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = configuration["IdentityServerURL"];
    options.Audience = "resource_basket";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddScoped<IBasketService,BasketService>();
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisContext>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

    var redis = new RedisContext(redisSettings.Host, redisSettings.Port);

    redis.Connect();

    return redis;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
