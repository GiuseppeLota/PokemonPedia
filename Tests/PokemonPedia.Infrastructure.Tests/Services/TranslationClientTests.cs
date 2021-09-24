using Moq;
using Newtonsoft.Json;
using PokemonPedia.Core.Components;
using PokemonPedia.Infrastructure.Data;
using PokemonPedia.Infrastructure.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PokemonPedia.Infrastructure.Tests.Services
{
    public class TranslationClientTests
    {
        private readonly string TEST_URI;
        private const string TEST_DESCRIPTION = "TEST_DESCRIPTION";
        private const string TRANSLATED_DESCR = "TRANSLATED_DESCR";

        private readonly Mock<PokepediaHttpClientFactory> _pokepediaHttpClientFactory;

        public TranslationClientTests() 
        {
            TEST_URI = new Uri("https://api.test").ToString();
            _pokepediaHttpClientFactory = new Mock<PokepediaHttpClientFactory>();
        }

        [Fact]
        public void No_error_should_return_a_translation()
        {
            var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
            {
                var traslationModel = new ExternalTranslationModel() { Contents = new Contents() { Translated = TRANSLATED_DESCR } };

                var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(traslationModel))
                };

                return await Task.FromResult(responseMessage);
            }));

            _pokepediaHttpClientFactory.Setup(x => x.GetHttpClient()).Returns(httpClient);

            var translationClient = new TranslationClient(_pokepediaHttpClientFactory.Object);

            var result = translationClient.TranslationFor(TEST_DESCRIPTION, TEST_URI).GetAwaiter().GetResult();

            Assert.Equal(TRANSLATED_DESCR, result);
        }

        [Fact]
        public void Error_should_return_the_untraslated_description()
        {
            var httpClient = new HttpClient(new HttpMessageHandlerStub(async (request, cancellationToken) =>
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }));

            _pokepediaHttpClientFactory.Setup(x => x.GetHttpClient()).Returns(httpClient);

            var translationClient = new TranslationClient(_pokepediaHttpClientFactory.Object);

            var result = translationClient.TranslationFor(TEST_DESCRIPTION, TEST_URI).GetAwaiter().GetResult();

            Assert.Equal(TEST_DESCRIPTION, result);
        }
    }
}
