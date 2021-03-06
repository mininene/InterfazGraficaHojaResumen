using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using WebResumen.Models;
using WebResumen.Services.Authentication;
using WebResumen.Services.Authorization;
using WebResumen.Services.LogError;
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
            //services.AddDataProtection()
            //    .PersistKeysToFileSystem(new DirectoryInfo(Configuration["SecuritySettings:PersistKey"]));


            services.AddDbContext<AppDbContext>(options => options  //Crear el context desde la base de datos
            .UseSqlServer(Configuration
            .GetConnectionString("DefaultConnection")));



            // Configure your policies
            services.AddAuthorization(options =>
            {

                //check Authorization view and controllers
                options.AddPolicy("AdminSupervisor", policy =>
                policy.Requirements.Add(new ADGroupASRequirement(Configuration["SecuritySettings:ADGroupAdmins"], Configuration["SecuritySettings:ADGroupSupervisors"])));

                options.AddPolicy("Admins", policy =>
                policy.Requirements.Add(new ADGroupRequirement(Configuration["SecuritySettings:ADGroupAdmins"])));

                options.AddPolicy("Users", policy =>
                policy.Requirements.Add(new ADGroupRequirement(Configuration["SecuritySettings:ADGroupUsers"])));


            });
            services.AddSingleton<IAuthorizationHandler, ADGroupUsersHandler>(); // check admins and users group for view and controllers         
            services.AddSingleton<IAuthorizationHandler, ADGroupASHandler>();  //check AdminSupervisor group for view and controllers


            services.AddScoped(typeof(IPrinterOchoVeinte), typeof(PrinterOchoVeinte));
            services.AddScoped(typeof(IPrinterDosTresCuatro), typeof(PrinterDosTresCuatro));
            services.AddScoped(typeof(IPrinterNueveDiez), typeof(PrinterNueveDiez));
            services.AddScoped(typeof(IPrinterOchoVeinteAS), typeof(PrinterOchoVeinteAS));
            services.AddScoped(typeof(IPrinterDosTresCuatroAS), typeof(PrinterDosTresCuatroAS));
            services.AddScoped(typeof(IPrinterNueveDiezAS), typeof(PrinterNueveDiezAS));

            services.AddSingleton(typeof(ILogErrors), typeof(LogErrors));



            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<TestManager>();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {

                options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(Configuration["SecuritySettings:IdleTime"]));//You can set Time   
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Expiration = TimeSpan.FromMinutes(Convert.ToInt32(Configuration["SecuritySettings:IdleTime"]));
               });


            services.AddScoped(typeof(ILogRecord), typeof(LogRecord));

            services.AddControllersWithViews()
                 .AddSessionStateTempDataProvider();
            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = " <span>&darr;</span>";
                options.HtmlIndicatorUp = " <span>&uarr;</span>";
            });

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

                app.UseExceptionHandler("/Homexx/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseStatusCodePagesWithRedirects("/Homexx/Error?code={0}");
            app.UseStatusCodePagesWithReExecute("/Homexx/Error", "?code={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession(); // // IMPORTANT: This session call MUST go before UseMvc()


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Inicio}/{action=Index}/{id?}");
            });
        }
    }
}
