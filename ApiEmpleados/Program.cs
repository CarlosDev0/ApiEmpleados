global using Microsoft.EntityFrameworkCore;
global using ApiEmpleados.Data;
global using ApiEmpleados.Service;
using ApiEmpleados.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using ApiEmpleados.DependencyInjection;
using ApiEmpleados.Redis;
using StackExchange.Redis;
using Microsoft.FeatureManagement;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddDbContext<DataContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Redis:
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(new RedisHelper(builder.Configuration).GetRedisConfiguration()));


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "valkey-apiempleados"; // Optional prefix for cache keys
});
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example: "bearar {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime= false,
        };
    });
builder.Services.AddAutoMapper(typeof(Program).Assembly);



builder.Services.AddScoped<IRegistroService, RegistroService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IAssessmentService, AssessmentService>();
builder.Services.AddSingleton<IMongoDBRepo, MongoDBRepo>();


builder.Services.AddApiEmpleadosServices();
//builder.Services.AddSingleton<IDb<Pago>, Db<Pago>>();
//builder.Services.AddScoped<IPagoService, PagoService>();

// Change the host to listen on all available network interfaces
builder.WebHost.UseUrls("http://0.0.0.0:5000");  //useful when running on Docker.

builder.Services.AddMemoryCache();

builder.Services.AddFeatureManagement();

var app = builder.Build();

// Migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    try
    {
        dbContext.Database.EnsureCreated();
        //dbContext.Database.Migrate(); // Apply migrations
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error with migrations: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //http://localhost:5000/swagger
}
//Middlewares:
app.UseHttpsRedirection();

// Shows UseCors with CorsPolicyBuilder.
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
