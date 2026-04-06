using HCMCMetroKioskV1.Data;
using HCMCMetroKioskV1.Models;
using HCMCMetroKioskV1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var mvcBuilder = builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}
builder.Services.AddDbContext<KioskDbContext>(o =>
    o.UseInMemoryDatabase("HCMCMetroKiosk"));

builder.Services.AddScoped<TicketService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<KioskDbContext>();
    db.Database.EnsureCreated();

    if (!db.Stations.Any())
    {
        // stations and fare rules seed...
        // (the full block from earlier)
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();