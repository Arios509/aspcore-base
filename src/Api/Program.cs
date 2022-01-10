using Api.Infrastructure;
using Api.Infrastructure.AutofacModules;
using Api.Infrastructure.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Aggregate.SmsMessage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddOptions().Configure<ConnectionStringOptions>(configuration.GetSection("ConnectionString"));

// postgres connection
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(configuration.GetSection("ConnectionString")["DefaultConnection"]);
});

builder.Services.AddScoped<ISmsMessageRepository, SmsMessageRepository>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new MediatorModule("Api"));
});

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
