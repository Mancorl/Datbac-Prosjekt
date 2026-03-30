using Unhosted_Api.Controllers;
using Unhosted_Api.Services;
using Unhosted_Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<HelloService>();     
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Data/unhosted.db")
);
builder.Services.AddSingleton<IFileUploadService, FileUploadService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });



var app = builder.Build();

app.MapControllers(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapControllers();  
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();



app.Run();


