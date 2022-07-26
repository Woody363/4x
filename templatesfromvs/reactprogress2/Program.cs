using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.EntityFrameworkCore;
using reactprogress2;
using System.Diagnostics;
using reactprogress2.Data;
using reactprogress2.Data.DataQueries;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();
AppSettings appSettings = builder.Configuration.GetSection("appSettings").Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>();//This creates a dependency injection for constructor asking for it generated from here
builder.Services.AddDbContext<PostgresContext>(options =>options.UseNpgsql(appSettings.ConnectionStrings.WoodyServer));
builder.Services.AddScoped<TestQueries>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
