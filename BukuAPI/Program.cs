global using BukuAPI.Models;
using BukuAPI.Services.BukuService;
using BukuAPI.Services.LogService;
using BukuAPI.Services.OrderService;
using BukuAPI.Services.SqlService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBukuService, BukuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<Logger>();
builder.Services.AddScoped<ISqlService, SqlService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
