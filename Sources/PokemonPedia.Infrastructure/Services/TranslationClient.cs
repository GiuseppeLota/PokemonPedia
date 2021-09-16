using Newtonsoft.Json;
using PokemonPedia.Infrastructure.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokemonPedia.Infrastructure.Services
{
    public class TranslationClient
    {
        /// <summary>
        /// Provide a translation for a provided description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<string> TranslationFor(string description, string translatorUri) 
        {
            using HttpClient client = new HttpClient();

            var objAsJson = JsonConvert.SerializeObject(new { text = description });
            var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(translatorUri, content);
            var result = JsonConvert.DeserializeObject<ExternalTranslationModel>(response.Content.ReadAsStringAsync().Result);

            if (HasError(result)) 
            {
                return description;
            }

            return result.Contents.Translated;
        }

        private bool HasError(ExternalTranslationModel result)
        {
            return !string.IsNullOrEmpty(result?.Error?.Message);
        }
    }
}
