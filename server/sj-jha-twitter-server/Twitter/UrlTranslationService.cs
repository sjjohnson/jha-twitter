using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace sj_jha_twitter_server.Twitter
{
    public class UrlTranslationService : IUrlTranslationService
    {
        private HttpClient _httpClient;

        public HttpMessageHandler MessageHandler { get; set; }
            = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };

        private HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    var handler = MessageHandler;

                    _httpClient = new HttpClient(handler)
                    {
                        BaseAddress = new Uri("https://t.co", UriKind.Absolute)
                    };
                }
                return _httpClient;
            }
        }

        public async Task<string> TranslateUrlAsync(string twitterPath)
        {
            using var req = new HttpRequestMessage(HttpMethod.Head, twitterPath);

            using var resp = await HttpClient.SendAsync(req);

            return resp.StatusCode == HttpStatusCode.MovedPermanently
                ? resp.Headers.Location.AbsoluteUri
                : null;

        }
    }
}
