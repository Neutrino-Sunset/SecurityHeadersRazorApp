using Microsoft.EntityFrameworkCore;

using Piranha;
using Piranha.AspNetCore.Identity.SQLite;
using Piranha.Data.EF.SQLite;



// This initialization adds the specified security headers to all requests.
/*
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(option => option.AddServerHeader = false);
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Error");
   app.UseHsts();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
   context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
   context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
   context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
   context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
   await next.Invoke();
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();

*/

// This minimal Piranha initialization adds the security headers to page requests, but not
// to the requests for any static resources.
var builder = WebApplication.CreateBuilder(args);

builder.AddPiranha(options =>
{
   options.UseCms();
   options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
   options.UseEF<SQLiteDb>(db =>
       db.UseSqlite("Filename=./PiranhaWeb.db"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Error");
   app.UseHsts();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
   context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
   context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
   context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
   context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
   await next.Invoke();
});

app.MapRazorPages();

app.UsePiranha(options =>
{
   App.Init(options.Api);
});

app.Run();
