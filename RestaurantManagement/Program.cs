using System.Text;
using Microsoft.OpenApi.Models;
using RestaurantManagement.API;
using RestaurantManagement.BLL.BLs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.DAL.Database;
using RestaurantManagement.DAL.Extensions;
using RestaurantManagement.BLL.SecureProxies;
using RestaurantManagement.BLL.Managers.Contracts;
using RestaurantManagement.Core.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantManagement.Core.Models.OptionsModels;
using RestaurantManagement.BLL.Managers.Implementation;
using RestaurantManagement.Core.Services.Contracts.BLs;
using RestaurantManagement.Core.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(o=>o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddOptions()
    .Configure<JwtModel>(builder.Configuration.GetSection("Jwt"));


builder.Services.AddAuthorization();

builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddDefaultData(builder.Configuration);

builder.Services.AddScoped<IOrderBL, OrderBL>();
builder.Services.Decorate<IOrderBL, OrderBlProxy>();

builder.Services.AddScoped<IRestaurantBL, RestaurantBL>();
builder.Services.AddScoped<IOrderManager, OrderManager>();
builder.Services.AddScoped<IAuthBL, AuthBL>();
builder.Services.AddScoped<IOrderDetailsBL, OrderDetailsBL>();
builder.Services.Decorate<IOrderDetailsBL, OrderDetailsBlProxy>();


builder.Services.AddScoped<IAccessControlService, AccessControlService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

UpdateDatabase();

app.Run();


void UpdateDatabase()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<RestaurantManagementContext>();
    context.Database.Migrate();
    var commonContext = scope.ServiceProvider.GetRequiredService<CommonContext>();
    commonContext.Database.Migrate();
}