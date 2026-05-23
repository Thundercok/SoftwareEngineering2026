using Microsoft.EntityFrameworkCore;
using WebMVC_Core.Data;
using WebMVC_Core.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=LabAssignment.db"));

// Enable Session for Login Checks
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// SEED DATA BLOCK
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    if (!context.Items.Any())
    {
        for (int i = 1; i <= 20; i++)
        {
            context.Items.Add(new Item { ItemName = $"Product {i}", Size = i % 2 == 0 ? "Large" : "Small" });
        }
        for (int i = 1; i <= 20; i++)
        {
            context.Agents.Add(new Agent { AgentName = $"Agent {i}", Address = $"{i} Street, City" });
        }
        context.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

// Initialize Session Middleware
app.UseSession(); 

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();