using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Services;
using MoviesDomain.Interfaces;
using MoviesInfrastructure;
using MoviesInfrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(container =>
    {
        container.RegisterType<MoviesService>()
            .As<IMoviesService>()
            .SingleInstance();

        container.RegisterType<MoviesRepository>()
            .As<IMoviesRepository>()
            .SingleInstance();
    });

builder.Services.AddDbContext<MoviesDbContext>(options =>
{
    var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

    if(defaultConnection != null)
    options.UseMySQL(defaultConnection);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
