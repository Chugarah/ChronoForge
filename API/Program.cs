using System.Reflection;
using API.Helpers;
using API.Interfaces;
using Core;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Adding support for CORS
builder.Services.AddCors(options =>
{
    // Adding a new policy
    options.AddPolicy(
        "AllowNextJS",
        policy =>
        {
            // Adding support for the Next.js application
            policy
                .WithOrigins("http://localhost:3000", "http://localhost:3001")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});

// Adding support for controllers
builder.Services.AddControllers();

// Let's add support for Entity Framework Core,
var connectionString = builder.Configuration.GetConnectionString("SQLServer");

/*
 Let's register the services using Dependency Injection
 We are using the Composition Root pattern and only for DI registration
 It conflicts with Clean Architecture, but it's a good way to register services
 Its middle ground between Clean Architecture and DI Composition Root
 */
builder.Services.AddScoped<ICommonHelpers, CommonHelpers>();
builder.Services.AddCoreServices().AddInfrastructure(connectionString!);

// Adding support for OpenAPI
builder.Services.AddEndpointsApiExplorer();

// https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
builder.Services.AddSwaggerGen(c =>
{
    // Adding a new Swagger document
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "ChroneForge API",
            Version = "v1",
            Description = "A simple API for managing projects and statuses",
            Contact = new OpenApiContact()
            {
                Name = "Georgi Sundberg",
                Email = "georgi.sundberg@outlook.com",
                Url = new Uri("https://www.google.se"),
            },
        }
    );

    // Adding XML comments to Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Adding a new Swagger endpoint
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChronoForge API v1");
        // Adding OAuth support
        c.OAuthClientId("swagger-ui");
        // Adding OAuth scopes
        c.OAuthScopes("api1", "api2");
    });

    app.MapOpenApi();
}

// Adding support for routing
app.UseHttpsRedirection();

// Enable CORS after the HTTPS redirection
app.UseCors("AllowNextJS");

// Adding support for User Authentication
app.UseAuthorization();

// Adding support for routing
app.MapControllers();
app.Run();
