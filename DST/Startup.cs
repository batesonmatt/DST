using DST.Models.DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DST
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession(
                //options =>
                //{
                //    options.IdleTimeout = TimeSpan.FromMinutes(5);
                //}
                );

            services.AddControllersWithViews();

            // Enable dependency injection for DbContext objects.
            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDbContext")));

            // Make URLs lowercase and end with a trailing slash.
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
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
                /* Add a dedicated error page for production. */
                app.UseExceptionHandler("/Home/Error");

                /* The default HSTS value is 30 days. 
                 * You may want to change this for production scenarios.
                 * See https://aka.ms/aspnetcore-hsts. */
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            //app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                // Filtering route
                endpoints.MapControllerRoute(
                    name: "filtering",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/" +
                    "filter/type-{type}/catalog-{catalog}/constellation-{constellation}/season-{season}/trajectory-{trajectory}/" +
                    "local-{local}/visible-{visible}/rising-{rising}/hasname-{hasname}");

                // Paging route
                endpoints.MapControllerRoute(
                    name: "paging",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");

                // Details route
                endpoints.MapControllerRoute(
                    name: "details",
                    pattern: "{controller}/{action}/{cat}/{id}/{slug?}");

                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
