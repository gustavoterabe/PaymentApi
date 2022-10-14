using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payment.Application.Services;
using Payment.Domain.Interfaces;
using Payment.Domain.Models;
using Payment.Domain.ViewModel.Sale;
using Payment.Infrastructure.Context;
using Payment.Infrastructure.Repositories;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaymentApiContext>(options =>
{
    options.UseSqlServer("name=ConnectionStrings:DefaultConnection", opt =>
    {
        opt.CommandTimeout(180);
        opt.EnableRetryOnFailure(5);
    });
});
    
builder.Services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
builder.Services.AddScoped<IBaseRepository<Sale>, BaseRepository<Sale>>();
builder.Services.AddScoped<IBaseRepository<Salesman>, BaseRepository<Salesman>>();

builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddSingleton(new MapperConfiguration(config =>
{
    config.CreateMap<CreateSaleViewModel, Sale>();
}).CreateMapper());

WebApplication app = builder.Build();

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