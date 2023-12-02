using Bikijada_MVC.DAL;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Mvc;
using React.AspNet;
using JavaScriptEngineSwitcher.V8;
using Microsoft.AspNetCore.Http;


namespace Bikijada_MVC
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Add React.
            services.AddReact();

            //Add JsEngineSwitcher V8.
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName).AddV8();

            //Add MVC.
            services.AddControllersWithViews();
        }
    
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseReact(config =>
              {
            config.AddScript("~/js/App.jsx");
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            PopulateDatabase();
            app.Run();


            static void PopulateDatabase()
            {

                var context = new BikijadaContext();

                //context.Database.EnsureDeleted();

                context.Database.EnsureCreated();
            }
;

        }
 }
}
