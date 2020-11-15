using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebResumen.Models;
using WebResumen.Services.Authorization;

namespace WebResumen
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
            services.AddDbContext<AppDbContext>(options => options  //Crear el context desde la base de datos
            .UseSqlServer(Configuration
            .GetConnectionString("DefaultConnection")));

            // Add all of your handlers to DI.
            services.AddSingleton<IAuthorizationHandler, ADGroupHandler>();
            services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            // services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

            // Configure your policies
            services.AddAuthorization(options =>                        
            {
                options.AddPolicy("ADRoleOnly", policy =>
                    //policy.Requirements.Add(new CheckADGroupRequirement("GLOBAL\\ESSA-HojaResumen_Users")));
                    policy.RequireRole(Configuration["SecuritySettings:ADGroup"]));  //verifica el grupo desde el json
                options.AddPolicy("Readonly", policy =>
                              policy.RequireClaim("permission", "readOnly"));
                options.AddPolicy("Write", policy =>
                        policy.RequireClaim("permission", "write"));

            });

           

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
