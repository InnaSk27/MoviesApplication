using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Services;
using MoviesDomain.Interfaces;
using MoviesInfrastructure;
using MoviesInfrastructure.Repositories;
using MoviesDomain.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(container =>
    {
        container.RegisterInstance(jwtSettings)
            .As<JwtSettings>()
            .SingleInstance();
        
        container.RegisterType<MoviesService>()
            .As<IMoviesService>()
            .SingleInstance();

        container.RegisterType<MoviesRepository>()
            .As<IMoviesRepository>()
            .SingleInstance();

        container.RegisterType<UsersService>().As<IUsersService>().SingleInstance();
        container.RegisterType<StudioService>().As<IStudiosService>().SingleInstance();
        container.RegisterType<StudiosRepository>().As<IStudiosRepository>().SingleInstance();
        container.RegisterType<UsersRepository>().As<IUsersRepository>().SingleInstance();
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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Movies API", 
        Version = "v1",
        Description = "API for managing movies"
    });
    
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
       In = ParameterLocation.Header,
       Description = "Please enter a valid Token",
       Name = "Authorization",
       Type = SecuritySchemeType.Http,
       BearerFormat = "JWT",
       Scheme = "Bearer" 
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });

});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(jwtSettings.SecretKey ?? "")
        ),
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
