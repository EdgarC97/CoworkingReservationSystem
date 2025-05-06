using CoworkingReservationSystem.Data;
using CoworkingReservationSystem.Services;
using CoworkingReservationSystem.Repositories.Interfaces;
using CoworkingReservationSystem.Repositories;
using CoworkingReservationSystem.Services.Interfaces;
using CoworkingReservationSystem.Utils;
using CoworkingReservationSystem.Validators;
using DotNetEnv;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CoworkingReservationSystem.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

// Add services to the container
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        fv.AutomaticValidationEnabled = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(EnvironmentHelper.GetConnectionString()));

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();

// Add services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAuditService, AuditService>();

// Add JWT service
builder.Services.AddSingleton<JwtService>();

// Add seeders
builder.Services.AddScoped<UserSeeder>();
builder.Services.AddScoped<RoomSeeder>();
builder.Services.AddScoped<BookingSeeder>();
builder.Services.AddScoped<DatabaseSeeder>();

// Also register them as ISeeder for any code that needs all seeders
builder.Services.AddScoped<ISeeder, UserSeeder>();
builder.Services.AddScoped<ISeeder, RoomSeeder>();
builder.Services.AddScoped<ISeeder, BookingSeeder>();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = EnvironmentHelper.GetJwtIssuer(),
        ValidAudience = EnvironmentHelper.GetJwtAudience(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentHelper.GetJwtSecret()))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    // Only seed data in development environment
    if (app.Environment.IsDevelopment())
    {
        var databaseSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        await databaseSeeder.SeedAsync();
    }
}

app.Run();