using Newtonsoft.Json;
using PokemonPedia.Core.Components;
using PokemonPedia.Infrastructure.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services
{
    public class TranslationClient
    {
        private readonly HttpClient _httpClient;

        public TranslationClient(PokepediaHttpClientFactory pokepediaHttpClient)
        {
            _httpClient = pokepediaHttpClient.GetHttpClient();
        }

        /// <summary>
        /// provide a translation for an incoming description
        /// </summary>
        /// <param name="description"></param>
        /// <param name="translatorUri"></param>
        /// <returns></returns>
        public async Task<string> TranslationFor(string description, string translatorUri)
        {
            var objAsJson = JsonConvert.SerializeObject(new { text = description });
            var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(translatorUri, content);

            if (!response.IsSuccessStatusCode)
            {
                return description;
            }

            var rawResponseContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ExternalTranslationModel>(rawResponseContent);

            return result.Contents.Translated;
        }
    }
}
