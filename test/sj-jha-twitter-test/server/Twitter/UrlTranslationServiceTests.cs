using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_test.server.Twitter
{
    [TestClass]
    public class UrlTranslationServiceTests
    {
        [TestMethod]
        public async Task TranslateUrlAsync_Happy()
        {
            const string TestSourceUrl = "https://t.co/testpath";
            const string TestDestUrl = "https://some.other.location/some/path";

            var response = new HttpResponseMessage(HttpStatusCode.MovedPermanently);
            response.Headers.Add("Location", TestDestUrl);

            var messageHandler = new TestMessageHandler(response);

            var service = new UrlTranslationService
            {
                MessageHandler = messageHandler
            };

            var result = await service.TranslateUrlAsync(TestSourceUrl);

            Assert.AreEqual(TestDestUrl, result);
            Assert.AreEqual(TestSourceUrl, messageHandler.Request.RequestUri.ToString());
            Assert.AreEqual(HttpMethod.Head, messageHandler.Request.Method);
        }

        private class TestMessageHandler : HttpClientHandler
        {
            private readonly HttpResponseMessage _response;

            public TestMessageHandler(HttpResponseMessage response)
            {
                AllowAutoRedirect = false;
                _response = response;
            }

            public HttpRequestMessage Request { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
            {
                Request = req;

                return Task.FromResult(_response);
            }
        }
    }
}
