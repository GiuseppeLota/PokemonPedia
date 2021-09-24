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

        public HttpClient GetHttpClient() 
        {
            return _httpClient;  
        } 
    }
}
