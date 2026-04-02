using Hr.Application.interfaces;
using Hr.Infrastructure.DataAccess;
using Hr.MVC.Extensions;
using Hr.MVC.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConnectionString(builder); //extension
builder.Services.AddApplicationIdentity(); //extension
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllersWithViews();

builder.Services.AddLogging(); //add logging

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Common/Errors/Error", "?code={0}");
    app.UseHsts();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

await app.Services.AddScopeToUserAndRole(); //extension

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Common" })
    .WithStaticAssets();

app.Run();
