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
    db.Stations.AddRange(
        new Station { Id = 1,  Name = "Bến Thành",           NameEn = "Ben Thanh",             StationOrder = 1,  IsUnderground = true  },
        new Station { Id = 2,  Name = "Nhà hát Thành phố",   NameEn = "City Theatre",           StationOrder = 2,  IsUnderground = true  },
        new Station { Id = 3,  Name = "Ba Son",               NameEn = "Ba Son",                 StationOrder = 3,  IsUnderground = true  },
        new Station { Id = 4,  Name = "Công viên Văn Thánh", NameEn = "Van Thanh Park",         StationOrder = 4,  IsUnderground = false },
        new Station { Id = 5,  Name = "Tân Cảng",            NameEn = "Tan Cang",               StationOrder = 5,  IsUnderground = false },
        new Station { Id = 6,  Name = "Thảo Điền",           NameEn = "Thao Dien",              StationOrder = 6,  IsUnderground = false },
        new Station { Id = 7,  Name = "An Phú",              NameEn = "An Phu",                 StationOrder = 7,  IsUnderground = false },
        new Station { Id = 8,  Name = "Rạch Chiếc",          NameEn = "Rach Chiec",             StationOrder = 8,  IsUnderground = false },
        new Station { Id = 9,  Name = "Phước Long",          NameEn = "Phuoc Long",             StationOrder = 9,  IsUnderground = false },
        new Station { Id = 10, Name = "Bình Thái",           NameEn = "Binh Thai",              StationOrder = 10, IsUnderground = false },
        new Station { Id = 11, Name = "Thủ Đức",             NameEn = "Thu Duc",                StationOrder = 11, IsUnderground = false },
        new Station { Id = 12, Name = "Khu Công nghệ cao",   NameEn = "Hi-Tech Park",           StationOrder = 12, IsUnderground = false },
        new Station { Id = 13, Name = "Đại học Quốc gia",   NameEn = "VNU Station",            StationOrder = 13, IsUnderground = false },
        new Station { Id = 14, Name = "Bến xe Suối Tiên",   NameEn = "Suoi Tien Bus Terminal", StationOrder = 14, IsUnderground = false }
    );
    db.SaveChanges();
}

if (!db.FareRules.Any())
{
    var matrix = new int[14, 14]
    {
        {  0,  7,  7,  7,  7,  7,  7,  9, 10, 12, 14, 16, 18, 20 },
        {  7,  0,  7,  7,  7,  7,  7,  8, 10, 11, 13, 16, 17, 20 },
        {  7,  7,  0,  7,  7,  7,  7,  7,  9, 10, 12, 15, 16, 18 },
        {  7,  7,  7,  0,  7,  7,  7,  7,  7,  8, 10, 13, 14, 17 },
        {  7,  7,  7,  7,  0,  7,  7,  7,  7,  7,  9, 12, 13, 16 },
        {  7,  7,  7,  7,  7,  0,  7,  7,  7,  7,  8, 10, 12, 14 },
        {  7,  7,  7,  7,  7,  7,  0,  7,  7,  7,  7,  9, 11, 13 },
        {  9,  8,  7,  7,  7,  7,  7,  0,  7,  7,  7,  8,  9, 11 },
        { 10, 10,  9,  7,  7,  7,  7,  7,  0,  7,  7,  7,  8, 10 },
        { 12, 11, 10,  8,  7,  7,  7,  7,  7,  0,  7,  7,  7,  8 },
        { 14, 13, 12, 10,  9,  8,  7,  7,  7,  7,  0,  7,  7,  7 },
        { 16, 16, 15, 13, 12, 10,  9,  8,  7,  7,  7,  0,  7,  7 },
        { 18, 17, 16, 14, 13, 12, 11,  9,  8,  7,  7,  7,  0,  7 },
        { 20, 20, 18, 17, 16, 14, 13, 11, 10,  8,  7,  7,  7,  0 },
    };

    var rules = new List<FareRule>();
    int id = 1;
    for (int f = 0; f < 14; f++)
    for (int t = 0; t < 14; t++)
    {
        if (f == t) continue;
        int cash = matrix[f, t] * 1000;
        rules.Add(new FareRule
        {
            Id               = id++,
            FromStationOrder = f + 1,
            ToStationOrder   = t + 1,
            CashPrice        = cash,
            NonCashPrice     = cash - 1000
        });
    }
    db.FareRules.AddRange(rules);
    db.SaveChanges();
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