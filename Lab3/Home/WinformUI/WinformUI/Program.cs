using Microsoft.EntityFrameworkCore;
using DAL;
using BLL;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure SQL Server cho Docker
builder.Services.AddDbContext<Exer1DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));

// 2. Register 3-Tier Layers
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

// 3. Register Session
builder.Services.AddSession();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 4. Configure Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();