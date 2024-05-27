using JwtAuthenticationManager.Domain.Repositories;
using JwtAuthenticationManager.Domain.Services;
using JwtAuthenticationManager.Mapping;
using JwtAuthenticationManager.Persistence.Context;
using JwtAuthenticationManager.Persistence.Repositories;
using JwtAuthenticationManager.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SecurityDbContext>(
    options => options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<SecurityDbContext>())
{
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();
app.MapControllers();

app.Run();