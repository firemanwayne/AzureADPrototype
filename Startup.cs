using AzureADPrototype.Data;
using AzureADPrototype.Delegates;
using AzureADPrototype.StateProviders.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzureADPrototype
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(o => 
            {
                o.AddConsole();
                o.AddEventLog();
                o.AddDebug();
                o.AddEventSourceLogger();
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddSingleton<MessageGenerator>();

            services.AddSingleton<NotificationState>();
            services.AddScoped<ITestClient, TestClient>();

            services.AddHostedService<MessageWorker>();
            services.AddHostedService<RecieverWorker>();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())            
                app.UseDeveloperExceptionPage();            
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}