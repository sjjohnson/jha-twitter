using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using sj_jha_twitter_server.Services;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_server
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(opt => opt.AddConsole().AddDebug());

            services.Configure<TwitterSettings>(s => Configuration.GetSection("TwitterSettings").Bind(s));

            services.AddSingleton<ITwitterStatsService, TwitterStatsService>();
            services.AddSingleton<IUrlTranslationService, UrlTranslationService>();
            services.AddSingleton<ITwitterStreamHandler, TwitterStreamHandler>();

            services.AddHostedService<TwitterApiService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
