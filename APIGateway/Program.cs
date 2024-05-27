using JwtAuthenticationManager.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

await app.UseOcelot();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
