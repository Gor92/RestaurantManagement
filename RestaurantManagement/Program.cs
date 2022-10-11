
using RestaurantManagement.API;
using RestaurantManagement.BLL.BLs;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Database;
using RestaurantManagement.DAL.Extensions;
using RestaurantManagement.BLL.SecureProxies;
using RestaurantManagement.Core.Services.Contracts;
using RestaurantManagement.Core.Services.Contracts.BLs;
using RestaurantManagement.RestaurantIdentification.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IMapper,Mapper>();

//builder.Services.AddDefaultData(builder.Configuration);

//builder.Services.AddScoped<IOrderBL, OrderBL>();
//builder.Services.Decorate<IOrderBL, OrderBlProxy>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


UpdateDatabase();
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


app.Run();


void UpdateDatabase()
{
    var dbContext = app.Services.GetService<RestaurantManagementContext>();
    dbContext?.Database.Migrate();
}