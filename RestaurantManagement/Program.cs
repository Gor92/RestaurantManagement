using RestaurantManagement.API;
using RestaurantManagement.BLL.BLs;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Database;
using RestaurantManagement.DAL.Extensions;
using RestaurantManagement.BLL.SecureProxies;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;
using RestaurantManagement.Core.Services.Implementation;
using RestaurantManagement.RestaurantIdentification.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IMapper,Mapper>();

builder.Services.AddDefaultData(builder.Configuration);

builder.Services.AddScoped<IOrderBL, OrderBL>();
builder.Services.Decorate<IOrderBL, OrderBlProxy>();

builder.Services.AddScoped<IRestaurantBL, RestaurantBL>();
builder.Services.AddScoped<IOrderDetailsBL, OrderDetailsBL>();
builder.Services.Decorate<IOrderDetailsBL, OrderDetailsBlProxy>();


builder.Services.AddScoped<IAccessControlService, AccessControlService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

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

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

UpdateDatabase();

app.Run();


void UpdateDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<RestaurantManagementContext>();
        context?.Database.Migrate();
    }
}