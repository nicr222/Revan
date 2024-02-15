using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MidStateShuttleServiceContextConnection") ?? throw new InvalidOperationException("Connection string 'MidStateShuttleServiceContextConnection' not found.");

builder.Services.AddDbContext<MidStateShuttleServiceContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MidStateShuttleServiceUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MidStateShuttleServiceContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
