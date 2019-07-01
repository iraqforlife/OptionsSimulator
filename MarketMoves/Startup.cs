using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketMoves.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MarketMoves.Models;
using MarketMoves.Areas.Identity;
using MarketMoves.Util;
namespace MarketMoves
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //deloy
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( Configuration.GetConnectionString("DeployementConnection")));
            //dev
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<Account>().AddDefaultUI(UIFramework.Bootstrap4).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<Account,IdentityRole>().AddDefaultUI(UIFramework.Bootstrap4).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var google = Configuration.GetSection("Google");
            services.AddAuthentication().AddGoogle(o =>
            {
                o.ClientId = google["ClientId"];
                o.ClientSecret = google["ClientSecret"];
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", policy => policy.RequireRole(Roles.Admin));
                options.AddPolicy("PaidAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole(Roles.Admin)
                                || context.User.IsInRole(Roles.PaidUser)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var serviceProvider = app.ApplicationServices;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            //Roles Configuration
            AsyncHelper.RunSync(() => SeedDatabaseAsync(serviceProvider));
        }
        private async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<Account>>();
                // Move this call out of the condition to automatically migrate development database.
                dbContext.Database.Migrate();

                if (!dbContext.Roles.Any(r => r.Name == Roles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                }
                if (!dbContext.Roles.Any(r => r.Name == Roles.PaidUser))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.PaidUser));
                }

                var adminEmails = Configuration.GetSection("AdminEmails").Get<List<string>>();
                var admins = dbContext.Users.Where(u => adminEmails.Contains(u.Email, StringComparer.InvariantCultureIgnoreCase)).ToList();

                foreach (var user in admins)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }

                var adminIds = admins.Select(a => a.Id).ToList();
                var nonAdmins = dbContext.Users.Where(u => !adminIds.Contains(u.Id)).ToList();

                foreach (var nonAdmin in nonAdmins)
                {
                    await userManager.RemoveFromRoleAsync(nonAdmin, Roles.Admin);
                }
            }
        }
    }
}

