using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Services;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"),
        migration => migration.MigrationsAssembly("Infrastructure")),
    ServiceLifetime.Scoped
);

builder.Services.AddScoped<Authentication>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    auth => auth.GetRequiredService<Authentication>()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
