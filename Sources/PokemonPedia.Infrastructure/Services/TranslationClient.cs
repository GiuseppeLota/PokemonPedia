using Newtonsoft.Json;
using PokemonPedia.Infrastructure.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services
{
    public class TranslationClient
    {
        private readonly HttpClient _httpClient;

        public TranslationClient(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// Provide a translation for a provided description
        /// </summary>
        /// <param name="description"></param>
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

            var result = JsonConvert.DeserializeObject<ExternalTranslationModel>(response.Content.ReadAsStringAsync().Result);

            return result.Contents.Translated;
        }
    }
}
