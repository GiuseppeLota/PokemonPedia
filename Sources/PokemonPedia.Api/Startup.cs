using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Services;
using PokemonPedia.Core.Interfaces;
using PokemonPedia.Infrastructure.Services;
using PokemonPedia.Infrastructure.Services.Strategies;
using System;

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
            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton<IPokemonService, PokemonService>();
            services.AddSingleton<IPokemonProvider, PokemonProvider>();
            services.AddSingleton<TranslationClient>();
            services.AddSingleton<YodaTranslationProvider>();
            services.AddSingleton<ShakespeareTranslationProvider>();
            services.AddSingleton<Func<string, ITranslationProvider>>(serviceProvider => habitat =>
            {
                return habitat switch
                {
                    "cave" => serviceProvider.GetService<YodaTranslationProvider>(),
                    _ => serviceProvider.GetService<ShakespeareTranslationProvider>()
                };
            });
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
