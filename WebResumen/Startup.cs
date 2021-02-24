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



           
           
            // Configure your policies
            services.AddAuthorization(options =>                        
            {
                //Para controladores
                //options.AddPolicy("ADTodos", policy =>
                //policy.Requirements.Add(new ADGroupAllRequirement("GLOBAL\\ESSA-HojaResumen_Users", "GLOBAL\\ESSA-HojaResumen_Admins", "GLOBAL\\ESSA-HojaResumen_Supervisors")));
                               

                options.AddPolicy("ADMIN", policy =>
                policy.Requirements.Add(new ADGroupAdminsRequirement("GLOBAL\\ESSA-HojaResumen_Admins"))); // controller con NT

                //VISTAS
                options.AddPolicy("AdminSupervisor", policy =>
                policy.Requirements.Add(new ADGroupASRequirement("ESSA-HojaResumen_Admins", "ESSA-HojaResumen_Supervisors"))); //vista con AD

                options.AddPolicy("Admins", policy =>
                policy.Requirements.Add(new ADGroupRequirement("ESSA-HojaResumen_Admins")));  //vista con AD
            
                options.AddPolicy("Users", policy =>
                policy.Requirements.Add(new ADGroupRequirement("ESSA-HojaResumen_Users")));  //vista con AD

                //options.AddPolicy("SUP", policy =>
                //   policy.Requirements.Add(new ADGroupRequirement("ESSA-HojaResumen_Supervisors")));

                //  options.AddPolicy("JustAdmin", policy =>
                //policy.Requirements.Add(new ADGroupRequirement("ESSA-HojaResumen_Admins")));

                //Para control de vistas
                //options.AddPolicy("ADUsers", policy =>
                //   policy.RequireRole(Configuration["SecuritySettings:ADGroupUsers"]));  //verifica el grupo desde el json

                //options.AddPolicy("ADMIN", policy =>
                //   policy.RequireRole(Configuration["SecuritySettings:ADGroupAdmins"]));

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
            services.AddSingleton<IAuthorizationHandler, ADGroupUsersHandler>(); // con esta llamada muevo las tres vistas
            //services.AddSingleton<IAuthorizationHandler, ADGroupAdminsHandler>();
            //services.AddSingleton<IAuthorizationHandler, ADGroupSupervisorsHandler>();

            //services.AddSingleton<IAuthorizationHandler, ADGroupAllHandler>();
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
                                
                options.IdleTimeout = TimeSpan.FromMinutes(120);//You can set Time   
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Expiration = TimeSpan.FromMinutes(120);
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
