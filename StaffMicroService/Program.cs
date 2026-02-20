using FluentValidation;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StaffMicroService.DatabaseContext;
using StaffMicroService.Middlewares;
using StaffMicroService.Repositories.Implementation;
using StaffMicroService.Repositories.Interfaces;
using StaffMicroService.Services.Implementation;
using StaffMicroService.Services.Integrations.StudentMicroService;
using StaffMicroService.Services.Interfaces;
using StudentMangementSystem.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adding connection string for the DBContext.
builder.Services.AddDbContext<StaffDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StaffDb")));

// Dependency Injection of Repositories
builder.Services.AddSingleton<CurrentUserService>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IApiLogRepository, ApiLogRepository>();

// Dependency Injection of Services
builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddHttpClient<IStudentService, StudentServiceClient>();

// Register Student Service Configuration
builder.Services.Configure<StudentServiceSettings>(
    builder.Configuration.GetSection("StudentService"));

// Dependecy Injection for the Automapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Dependecy Injdection for the Validation
//builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

// Global leve [Authorize] Attribute and no need to specify in all the controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// JWT Auth token validation
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// This will create the database tables automatically.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StaffDbContext>();
    db.Database.Migrate();
}

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

//// Registering Validation Middleware
//app.UseMiddleware<ValidationMiddleware>();

// Registering the Global Exception Middleware
//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Registering the LoggingMiddleware
app.UseMiddleware<ApiLoggingMiddleware>();

app.MapControllers();

app.Run();
