using FluentValidation;
using Microsoft.AspNetCore.Mvc.Authorization;
using StudentMangementSystem.Auth;
using StudentMangementSystem.Model.Log.Interface;
using StudentMicroService.DatebaseFactory;
using StudentMicroService.Infrastructure;
using StudentMicroService.Middlewares;
using StudentMicroService.Repositories.Implementation;
using StudentMicroService.Repositories.Interfaces;
using StudentMicroService.Services.Implementation;
using StudentMicroService.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Dependecy Injection for the Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Dependecy Injdection for the Validation
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<CurrentUserService>();
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IApiLogRepository, ApiLogRepository>();

// Global leve [Authorize] Attribute and no need to specify in all the controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// JWT Auth token validation
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

// Registering DatabaseMigrationRunner
builder.Services.AddTransient<DatabaseMigrationRunner>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.MapOpenApi();
app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/v1.json";
    options.Path = "/swagger";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Registering the LoggingMiddleware
app.UseMiddleware<ApiLoggingMiddleware>();

app.MapControllers();

//Below will run the scripts automatically incase if it found a new script version.
using (var scope = app.Services.CreateScope())
{
    try
    {
        Console.WriteLine("Resolving DatabaseMigrationRunner");

        var runner = scope.ServiceProvider
                          .GetRequiredService<DatabaseMigrationRunner>();

        Console.WriteLine("Calling Run()");
        runner.Run();
    }
    catch (Exception excp)
    {

    }
}
app.Run();
