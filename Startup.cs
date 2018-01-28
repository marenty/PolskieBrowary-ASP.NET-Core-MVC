using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolskieBrowary.Data;
using PolskieBrowary.Models;
using PolskieBrowary.Services;

namespace PolskieBrowary
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
            services.AddDbContext<PolskieBrowaryContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider);

        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin",  "User" };
            Task<IdentityResult> roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = RoleManager.RoleExistsAsync(roleName);
                roleExist.Wait();

                if (!roleExist.Result)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = RoleManager.CreateAsync(new IdentityRole(roleName));
                    roleResult.Wait();
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new ApplicationUser
            {

                UserName = "admin@admin.pl",
                Email = "admin@admin.pl"
            };
            //Ensure you have these values in your appsettings.json file
            string userPWD = "Zaq12wsx@";
            var _user = UserManager.FindByEmailAsync("admin@admin.pl");
            _user.Wait();

            if (_user == null)
            {
                var createPowerUser = UserManager.CreateAsync(poweruser, userPWD);
                createPowerUser.Wait();
                if (createPowerUser.Result.Succeeded)
                {
                    //here we tie the new user to the role
                    Task<IdentityResult> newUserRole = UserManager.AddToRoleAsync(poweruser, "Admin");
                    newUserRole.Wait();

                }
            }
        }





    }
}
