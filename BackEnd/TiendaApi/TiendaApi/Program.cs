using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TiendaApi.Extensions;
using TiendaApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

//Aplicar migraciones y crear bd si es necesario
using var scope =  app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<TiendaContext>();
var logger = services.GetRequiredService <ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await TiendaContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "Un error ocurio durante la migracion");
}


app.Run();
