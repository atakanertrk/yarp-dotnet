using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace yarp_demo_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddReverseProxy()
             .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("customPolicy", opt =>
                {
                    opt.PermitLimit = 4;
                    opt.Window = TimeSpan.FromSeconds(12);
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 2;
                });
            });

            var app = builder.Build();

            #region MinimalAPI Endpoints
            
            app.MapGet("/", () => "Hello World!");

            #endregion

            app.UseRateLimiter();

            // app.MapReverseProxy();
            app.MapReverseProxy(proxyPipeline =>
            {
                proxyPipeline.Use((context, next) =>
                {
                    // Custom inline middleware

                    return next();
                });
                proxyPipeline.UseSessionAffinity();
                proxyPipeline.UseLoadBalancing();
                proxyPipeline.UsePassiveHealthChecks();
            });

            app.Run();
        }
    }
}
