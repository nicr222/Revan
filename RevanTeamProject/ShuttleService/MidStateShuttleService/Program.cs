using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MidStateShuttleService.Areas.Identity.Data;
using MidStateShuttleService.Data;
using MidStateShuttleService.Service;
using MidStateShuttleService.Models.Data;
namespace MidStateShuttleService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var identityConnectionString = builder.Configuration.GetConnectionString("MidStateShuttleServiceContextConnection") ?? throw new InvalidOperationException("Connection string 'MidStateShuttleServiceContextConnection' not found.");
            var appConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<MidStateShuttleServiceContext>(options => options.UseSqlServer(identityConnectionString));
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(appConnectionString));

            builder.Services.AddDbContext<MidStateShuttleServiceContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddSingleton<IListService, ListServices>();
            builder.Services.AddDefaultIdentity<MidStateShuttleServiceUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MidStateShuttleServiceContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

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
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // add user Roles and a default Admin
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MidStateShuttleServiceUser>>();

                var roles = new[] { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                string email = "Admin@email.com";
                string password = "Admin1$";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new MidStateShuttleServiceUser
                    {
                        Email = email,
                        UserName = email,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            app.Run();
        }
    }
}



