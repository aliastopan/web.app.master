using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Infrastructure.Persistence;
using Infrastructure.Services;
// using Application.Services;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages( options =>
    options.RootDirectory = "/Pages/Host"
);
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"),
        migration => migration.MigrationsAssembly("Infrastructure")),
    ServiceLifetime.Scoped
);

// builder.Services.AddScoped<AuthenticationStateProvider, Authenticator>();
builder.Services.AddScoped<Authenticator>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    provider => provider.GetRequiredService<Authenticator>()
);
builder.Services.AddScoped<Authorizer>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
