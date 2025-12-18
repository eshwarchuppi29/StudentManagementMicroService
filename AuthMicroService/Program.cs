using AuthMicroService.Service;
using StudentMangementSystem.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Token Generation part
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtSettings>>().Value);
builder.Services.AddSingleton<TokenService>();

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

// Commented because this doesn't require Auth.
//app.UseAuthorization();

app.MapControllers();

app.Run();
