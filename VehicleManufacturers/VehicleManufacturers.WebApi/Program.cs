using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ninject;
using VehicleManufacturers.DAL;
using VehicleManufacturers.Service.Common;
using VehicleManufacturers.WebApi.RestModels;

var builder = WebApplication.CreateBuilder(args);

// Register the Automapper configuration
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<VehicleManufacturers.Repository.RepositoryProfile>();
    cfg.AddProfile<RestProfile>();
});
var mapper = mapperConfiguration.CreateMapper();

var kernel = new StandardKernel();

kernel.Bind<IConfiguration>().ToConstant(builder.Configuration).InSingletonScope();
kernel.Load<VehicleManufacturers.Repository.DIModule>();
kernel.Load<VehicleManufacturers.Service.DIModule>();

kernel.Bind<IMapper>().ToConstant(mapper).InSingletonScope();

// Add services to the container
builder.Services.AddSingleton<IKernel>(kernel);                      // Register DI container
builder.Services.AddScoped(_ => kernel.Get<IVehicleMakeService>());  // Service registration
builder.Services.AddScoped(_ => kernel.Get<IVehicleModelService>()); // Service registration

// Add Automapper configuration to DI container
builder.Services.AddSingleton(mapperConfiguration);

// Add Automapper as a service in DI container
builder.Services.AddScoped<IMapper>(sp => sp.GetRequiredService<MapperConfiguration>().CreateMapper());

// Add services to the container
builder.Services.AddDbContext<VehicleManufacturersContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the HTTP request pipeline
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();