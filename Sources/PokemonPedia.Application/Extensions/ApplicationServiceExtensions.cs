using Microsoft.Extensions.DependencyInjection;
using PokemonPedia.Application.Interfaces;
using PokemonPedia.Application.Services;
using PokemonPedia.Core.Interfaces;

namespace PokemonPedia.Application.Extensions
{

    public static class ApplicationServiceExtensions
    {
        public static void AddApplicationLevelServices(this IServiceCollection services)
        {
            services.AddSingleton<ITranslationResolver, TranslationResolver>();
            services.AddSingleton<IPokemonService, PokemonService>();
        }
    }

}
