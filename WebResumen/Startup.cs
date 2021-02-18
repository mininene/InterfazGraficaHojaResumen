using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebResumen.Models;
using WebResumen.Services.Authentication;
using WebResumen.Services.Authorization;
using WebResumen.Services.LogRecord;
using WebResumen.Services.PrinterService;
using WebResumen.Services.printerServiceAS;

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


            // services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            // services.AddTransient<IClaimsTransformation, ClaimsTransformer>();

            // Configure your policies
            services.AddAuthorization(options =>                        
            {
                //Para controladores
                options.AddPolicy("ADTodos", policy =>
                policy.Requirements.Add(new ADGroupAllRequirement("GLOBAL\\ESSA-HojaResumen_Users", "GLOBAL\\ESSA-HojaResumen_Admins", "GLOBAL\\ESSA-HojaResumen_Supervisors")));

                options.AddPolicy("ADAS", policy =>
                policy.Requirements.Add(new ADGroupASRequirement("GLOBAL\\ESSA-HojaResumen_Admins", "GLOBAL\\ESSA-HojaResumen_Supervisors")));

                options.AddPolicy("Admins", policy =>
                policy.Requirements.Add(new ADGroupAdminsRequirement("GLOBAL\\ESSA-HojaResumen_Admins")));


                //options.AddPolicy("ADTodos", policy =>
                //policy.Requirements.Add(new ADGroupAllRequirement("ESSA-HojaResumen_Users", "ESSA-HojaResumen_Admins", "ESSA-HojaResumen_Supervisors")));

                //options.AddPolicy("ADAS", policy =>
                //policy.Requirements.Add(new ADGroupASRequirement("ESSA-HojaResumen_Admins", "ESSA-HojaResumen_Supervisors")));

                //options.AddPolicy("Admins", policy =>
                //policy.Requirements.Add(new ADGroupAdminsRequirement("ESSA-HojaResumen_Admins")));


                //Para control de vistas
                options.AddPolicy("ADUsers", policy =>
                   policy.RequireRole(Configuration["SecuritySettings:ADGroupUsers"]));  //verifica el grupo desde el json

                options.AddPolicy("ADAdmins", policy =>
                   policy.RequireRole(Configuration["SecuritySettings:ADGroupAdmins"]));

                //options.AddPolicy("ADSupervisors", policy =>
                //   policy.RequireRole(Configuration["SecuritySettings:ADGroupSupervisors"]));

                //options.AddPolicy("ADTodos", policy =>
                //  policy.RequireRole("ADUsers", "ADAdmins","ADSupervisors"));


                //policy.Requirements.Add(new CheckADGroupRequirement("GLOBAL\\ESSA-HojaResumen_Users")));
                //options.AddPolicy("Readonly", policy =>
                //              policy.RequireClaim("permission", "readOnly"));
                //options.AddPolicy("Write", policy =>
                //        policy.RequireClaim("permission", "write"));

            });
            //services.AddSingleton<IAuthorizationHandler, ADGroupUsersHandler>();
            //services.AddSingleton<IAuthorizationHandler, ADGroupAdminsHandler>();
            //services.AddSingleton<IAuthorizationHandler, ADGroupSupervisorsHandler>();
            services.AddSingleton<IAuthorizationHandler, ADGroupAllHandler>();
            services.AddSingleton<IAuthorizationHandler, ADGroupASHandler>();
            services.AddSingleton<IAuthorizationHandler, ADGroupAdminsHandler>();

            services.AddScoped(typeof(IPrinterOchoVeinte), typeof(PrinterOchoVeinte));
            services.AddScoped(typeof(IPrinterDosTresCuatro), typeof(PrinterDosTresCuatro));
            services.AddScoped(typeof(IPrinterNueveDiez), typeof(PrinterNueveDiez));
            services.AddScoped(typeof(IPrinterOchoVeinteAS), typeof(PrinterOchoVeinteAS));
            services.AddScoped(typeof(IPrinterDosTresCuatroAS), typeof(PrinterDosTresCuatroAS));
            services.AddScoped(typeof(IPrinterNueveDiezAS), typeof(PrinterNueveDiezAS));



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<TestManager>();
            services.AddDistributedMemoryCache();
           
            services.AddSession(options => {

                
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Expiration = TimeSpan.FromMinutes(10);
                });


            services.AddScoped(typeof(ILogRecord), typeof(LogRecord));

            services.AddControllersWithViews()
                 .AddSessionStateTempDataProvider();

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
            app.UseSession(); // // IMPORTANT: This session call MUST go before UseMvc()
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
