﻿using Infrastructure;
using Presentation.Configurations.AutoFacts;
using Presentation.Configurations.JwtConfigs;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddJWTServices(builder.Configuration);
builder.Host.AddAutoFact();

var isDev = builder.Environment.IsDevelopment();
string? infrastructureDirectory;

if (isDev)
{
    infrastructureDirectory = InfraStructureHelper.GetInfrastructureDirectory();
}
else
{
    infrastructureDirectory = Directory.GetCurrentDirectory();
}
var settings = builder.Configuration
    .SetBasePath(infrastructureDirectory!)
    .AddJsonFile("InfraAppSetting.json", optional: false, reloadOnChange: true)
    .Build();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
