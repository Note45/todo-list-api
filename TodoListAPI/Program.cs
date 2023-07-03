using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoListAPI.Application.Services;
using TodoListAPI.Domain.Helpers;
using TodoListAPI.Domain.Repositories;
using TodoListAPI.Domain.Services;
using TodoListAPI.Infra;
using TodoListAPI.Infra.Database.Config;

var builder = WebApplication.CreateBuilder(args);

// Add database
builder.Services.AddSingleton<DbContext, DataContext>();

// Add services to the container.
builder.Services.AddSingleton<ITodoListRepository, TodoListRepository>();
builder.Services.AddSingleton<ITodoListService, TodoListService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.Configure<HashingOptions>(
    builder.Configuration.GetSection("HashingOptions"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();

