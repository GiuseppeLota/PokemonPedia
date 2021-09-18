using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokemonPedia.Application.Extensions;
using PokemonPedia.Core.Interfaces;
using PokemonPedia.Infrastructure.Services;
using PokemonPedia.Infrastructure.Services.Strategies;

namespace PokemonPedia.Api
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
            services.AddControllers();
            services.AddHttpClient<TranslationClient>();
            services.AddAutoMapper(typeof(Startup));
            services.AddApplicationLevelServices();
            services.AddSingleton<IPokemonProvider, PokemonProvider>();
            services.AddSingleton<TranslationClient>();
            services.AddSingleton<ITranslationProvider, YodaTranslationProvider>();
            services.AddSingleton<ITranslationProvider, ShakespeareTranslationProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
