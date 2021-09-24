using System.Net.Http;

namespace PokemonPedia.Core.Components
{
    public class PokepediaHttpClientFactory
    {
        private readonly HttpClient _httpClient;

        public PokepediaHttpClientFactory(IHttpClientFactory clientFactory) 
        {
            _httpClient = clientFactory.CreateClient();
        }

        public PokepediaHttpClientFactory() { }

        public virtual HttpClient GetHttpClient() 
        {
            return _httpClient;  
        } 
    }
}
