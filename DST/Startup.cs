using DST.Models.Builders;
using DST.Models.DataLayer;
using DST.Models.DataLayer.Repositories;
using DST.Models.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

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

            // Configure the cookie policy.
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Secure = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                    options.OnAppendCookie = cookieContext => cookieContext.SetPartitionedCookie();
                });

            // Configure session state options.
            services.AddSession(
                options =>
                {
                    //options.IdleTimeout = TimeSpan.FromMinutes(5);
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Path = "/; samesite=None; Partitioned";
                });

            // Configure antiforgery options.
            services.AddAntiforgery(
                options =>
                {
                    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Path = "/; samesite=None; Partitioned";
                });

            services.AddControllersWithViews();

            // Enable dependency injection and disable query tracking for DbContext objects.
            services.AddDbContext<MainDbContext>(options =>
            {
                /* Microsoft.EntityFrameworkCore.SqlServer v8.0.0
                 * SqlServerDbContextOptionsBuilder.UseCompatibilityLevel(Int32)
                 */
                options.UseSqlServer(Configuration.GetConnectionString("MainDbContext"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Configure dependency injection for the IHttpContextAccessor service.
            services.AddHttpContextAccessor();

            // Enable dependency injection for miscellaneous objects.
            services.AddTransient<ISearchUnitOfWork, SearchUnitOfWork>();
            services.AddTransient<ITrackUnitOfWork, TrackUnitOfWork>();
            services.AddTransient<ISearchRouteBuilder, SearchRouteBuilder>();
            services.AddTransient<IGeolocationBuilder, GeolocationBuilder>();
            services.AddTransient<ISearchBuilder, SearchBuilder>();
            services.AddTransient<ITrackPhaseBuilder, TrackPhaseBuilder>();
            services.AddTransient<ITrackPeriodBuilder, TrackPeriodBuilder>();
            // services.AddTransient<ISortFilterBuilder, SortFilterBuilder>();

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
#if DEBUG
                app.UseDeveloperExceptionPage();
#else
                app.UseExceptionHandler(
                    builder => builder.Run(async context => await Task.Run(() =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            context.Response.Redirect("/error");
                        }
                    })));
#endif
            }
            else
            {
                app.UseExceptionHandler(
                    builder => builder.Run(async context => await Task.Run(() =>
                    {
                        //context.Response.ContentType = Text.Html;
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            //if (error.Path.Contains("/home/", System.StringComparison.InvariantCultureIgnoreCase))
                            //{
                            //    context.Response.Redirect("/home/error");
                            //}
                            //else if (error.Path.Contains("/search/", System.StringComparison.InvariantCultureIgnoreCase))
                            //{
                            //    context.Response.Redirect("/search/error");
                            //}
                            //else
                            //{
                            //    context.Response.Redirect("/error");
                            //}

                            context.Response.Redirect("/error");
                        }
                    })));

                /* The default HSTS value is 30 days. 
                 * You may want to change this for production scenarios.
                 * See https://aka.ms/aspnetcore-hsts. */
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseCookiePolicy();
            //app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                // Track Period route
                endpoints.MapControllerRoute(
                    name: "trackperiod",
                    pattern: "{controller}/{action}/{catalog}/{id}/{algorithm}/format-{coordinateformat}/{start}/track-once-{trackonce}/fixed-tracking-{fixed}/aggregate-{aggregate}/{timeunit}/{period}/interval/{interval}");

                // Track Phase route
                endpoints.MapControllerRoute(
                    name: "trackphase",
                    pattern: "{controller}/{action}/{catalog}/{id}/{algorithm}/format-{coordinateformat}/{start}/{phase}/track-once-{trackonce}/cycles/{cycles}");

                // Track Summary route
                endpoints.MapControllerRoute(
                    name: "tracksummary",
                    pattern: "{controller}/{action}/{catalog}/{id}/{algorithm}");

                // Filtering route
                endpoints.MapControllerRoute(
                    name: "filtering",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/" +
                    "filter/type-{type}/catalog-{catalog}/constellation-{constellation}/season-{season}/trajectory-{trajectory}/" +
                    "visibility-{visibility}/hasname-{hasname}/{search?}");

                // Paging route
                //endpoints.MapControllerRoute(
                //    name: "paging",
                //    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}");

                // Details route
                endpoints.MapControllerRoute(
                    name: "details",
                    pattern: "{controller}/{action}/{catalog}/{id}");

                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
