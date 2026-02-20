using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using System;
using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Services;
using Utlånssystem_Konvensjonell.Core.Domain.Account.Handlers;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<RegistrationService>();
builder.Services.AddScoped<RegisterUserHandler>();


builder.Services.AddDbContext<BoardGameContext>(options =>
{
	options.UseSqlite($"Data Source={Path.Combine("Infrastructure", "Data", "BGDB.db")}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BoardGameContext>();
    db.Database.EnsureCreated(); // creates DB and tables if they don't exist
    var registrationService = scope.ServiceProvider.GetRequiredService<RegistrationService>();
    var handler = scope.ServiceProvider.GetRequiredService<RegisterUserHandler>();

    registrationService.Registered += handler.OnRegistered;
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
