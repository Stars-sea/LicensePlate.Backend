using LicensePlate.Server;
using LicensePlate.Server.Database;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddCors(builder.Configuration);

builder.Services.AddControllers().ConfigureJson();
builder.Services.AddIdentity();
builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddSettings(builder.Configuration);
builder.Services.AddServices();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

using (IServiceScope scope = app.Services.CreateScope())
using (ApplicationDb database = scope.ServiceProvider.GetRequiredService<ApplicationDb>()) {
    database.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
