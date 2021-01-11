using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sj_jha_twitter_server;
using sj_jha_twitter_server.Services;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_test.server.Services
{
    [TestClass]
    public class TwitterApiServiceTests
    {
        private const string EmojiTextBase64 = "QFBob2thVFNWIEBLYWl6ZXJDaGllZnMgRXZlbiBpZiB3ZSBhcmUgbGVhZCBieSBQZXAgd2lsbCBzdGlsbCBiZSBsb29zaW5nLi4KIEtjIGdvdCBpbnRlcm5hbCBwcm9ibGVtcyDwn5ip8J+kpvCfj73igI3imYLvuI8=";

        private readonly Mock<IOptions<TwitterSettings>> _optionsMock = new Mock<IOptions<TwitterSettings>>(MockBehavior.Strict);
        private readonly Mock<ILoggerFactory> _loggerFactoryMock = new Mock<ILoggerFactory>(MockBehavior.Strict);
        private readonly Mock<ITwitterStatsService> _statsServiceMock = new Mock<ITwitterStatsService>(MockBehavior.Strict);
        private readonly Mock<ITwitterStreamHandler> _streamHandlerMock = new Mock<ITwitterStreamHandler>(MockBehavior.Strict);
        private readonly Mock<IUrlTranslationService> _translationServiceMock = new Mock<IUrlTranslationService>(MockBehavior.Strict);
        private readonly Mock<Stream> _streamMock = new Mock<Stream>(MockBehavior.Strict);
        private readonly List<string> _expectedEmojis = new List<string> { "1f629", "1f926-1f3fd-200d-2642-fe0f" };
        private readonly List<string> _domains = new List<string> { $"{RndStr}.com", $"{RndStr}.com" };
        private readonly List<string> _hashtags = new List<string> { $"#{RndStr}", $"#{RndStr}" };
        private readonly List<string> _hosts;
        private readonly List<string> _urls;
        private readonly string _emojiText;
        private readonly string _tweetText;
        private readonly TwitterSettings _settings = new TwitterSettings
        {
            BaseUrl = $"https://{RndStr}.com",
            SampleStreamEndpoint = $"/{RndStr}"
        };

        private string _testToken;

        static TwitterApiServiceTests()
        {
            EmojiCatalog.AddName("1f629");
            EmojiCatalog.AddName("1f926-1f3fd-200d-2642-fe0f");
        }

        public TwitterApiServiceTests()
        {
            var bytes = Convert.FromBase64String(EmojiTextBase64);

            _emojiText = Encoding.UTF8.GetString(bytes);
            _hosts = new List<string> { $"www.{_domains[0]}", $"www.{_domains[1]}" };
            _urls = new List<string> { $"https://{_hosts[0]}", $"https://{_hosts[1]}" };
            _tweetText = $"emojiStart{_emojiText}emojiEnd urlStart{_urls[0]} {_urls[1]} urlEnd hashtagStart {_hashtags[0]} {_hashtags[1]} END";
        }

        private static string RndStr => $"{Guid.NewGuid():n}";

        [TestInitialize]
        public void TestInitialize()
        {
            var tokenVar = Environment.GetEnvironmentVariable(TwitterSettings.TwitterTokenEnvVar);
            if (string.IsNullOrEmpty(tokenVar))
            {
                tokenVar = RndStr;
                Environment.SetEnvironmentVariable(TwitterSettings.TwitterTokenEnvVar, tokenVar, EnvironmentVariableTarget.Process);
            }

            _testToken = tokenVar;

            _loggerFactoryMock
               .Setup(x => x.CreateLogger(typeof(TwitterApiService).FullName))
               .Returns(new Mock<ILogger>(MockBehavior.Loose).Object);

            _optionsMock
               .SetupGet(x => x.Value)
               .Returns(_settings);
        }

        [TestMethod]
        public void Constructor_Happy()
        {
            Assert.IsNotNull(
                new TwitterApiService(
                    _optionsMock.Object,
                    _loggerFactoryMock.Object,
                    _statsServiceMock.Object,
                    _streamHandlerMock.Object,
                    _translationServiceMock.Object));
        }

        [TestMethod]
        public void Constructor_NullOptions_ThrowsArgumentNullException()
        {
            var x = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterApiService(
                    null,
                    _loggerFactoryMock.Object,
                    _statsServiceMock.Object,
                    _streamHandlerMock.Object,
                    _translationServiceMock.Object));

            Assert.AreEqual("options", x.ParamName);
        }

        [TestMethod]
        public void Constructor_NullOptionsValue_ThrowsArgumentNullException()
        {
            _optionsMock
               .SetupGet(x => x.Value)
               .Returns((TwitterSettings)null);

            var ex = Assert.ThrowsException<ArgumentException>(
                () => new TwitterApiService(
                    _optionsMock.Object,
                    _loggerFactoryMock.Object,
                    _statsServiceMock.Object,
                    _streamHandlerMock.Object,
                    _translationServiceMock.Object));

            Assert.AreEqual("options", ex.ParamName);
            StringAssert.Contains(ex.Message, ".Value");
        }

        [TestMethod]
        public void Constructor_NullLoggerFactory_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterApiService(
                    _optionsMock.Object,
                    null,
                    _statsServiceMock.Object,
                    _streamHandlerMock.Object,
                    _translationServiceMock.Object));

            Assert.AreEqual("loggerFactory", ex.ParamName);
        }

        [TestMethod]
        public void Constructor_NullStatsService_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterApiService(
                    _optionsMock.Object,
                    _loggerFactoryMock.Object,
                    null,
                    _streamHandlerMock.Object,
                    _translationServiceMock.Object));

            Assert.AreEqual("statsService", ex.ParamName);
        }

        [TestMethod]
        public void Constructor_NullStreamHandler_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterApiService(
                    _optionsMock.Object,
                    _loggerFactoryMock.Object,
                    _statsServiceMock.Object,
                    null,
                    _translationServiceMock.Object));

            Assert.AreEqual("streamHandler", ex.ParamName);
        }

        [TestMethod]
        public void Constructor_NullTranslationService_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterApiService(
                    _optionsMock.Object,
                    _loggerFactoryMock.Object,
                    _statsServiceMock.Object,
                    _streamHandlerMock.Object,
                    null));

            Assert.AreEqual("urlTranslator", ex.ParamName);
        }

        [TestMethod]
        public async Task StartAsync_Happy()
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            _streamMock
               .SetupGet(x => x.CanSeek)
               .Returns(false);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(_streamMock.Object)
            };

            var handler = new TestMessageHandler(response, ct);
            var service = New();
            service.MessageHandler = handler;

            _streamHandlerMock
               .Setup(x => x.StartAsync(It.Is<Stream>(s => s != null), Encoding.UTF8, service))
               .Returns(Task.CompletedTask);

            await service.StartAsync(ct);

            Assert.AreEqual("Bearer", handler.Request.Headers.Authorization.Scheme);
            Assert.AreEqual(_testToken, handler.Request.Headers.Authorization.Parameter);
            Assert.AreEqual(HttpMethod.Get, handler.Request.Method);
            Assert.AreEqual($"{_settings.BaseUrl}{_settings.SampleStreamEndpoint}", handler.Request.RequestUri.ToString());

            System.Diagnostics.Debugger.Break();
        }

        [TestMethod]
        public async Task StopAsync_Happy()
        {
            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            _streamHandlerMock
               .Setup(x => x.StopAsync())
               .Returns(Task.CompletedTask);

            await New().StopAsync(ct);
        }

        [TestMethod]
        public void OnTweetReceived_Happy()
        {
            var tweet = new Tweet { Id = 87654, Text = _tweetText };

            _statsServiceMock
               .Setup(x => x.TweetReceived(It.IsAny<Tweet>()));

            New().OnTweetReceived(tweet);

            _statsServiceMock
               .Verify(
                    x => x.TweetReceived(It.Is<Tweet>(t => ValidateTweet(t))),
                    Times.Once);
        }

        private bool ValidateTweet(Tweet t)
        {
            Assert.AreEqual(_tweetText, t.Text);
            Assert.AreNotEqual(0, t.Id);

            Assert.IsNotNull(t.Emojis);
            Assert.AreEqual(_expectedEmojis.Count, t.Emojis.Count);
            CollectionAssert.Contains(t.Emojis, _expectedEmojis[0]);
            CollectionAssert.Contains(t.Emojis, _expectedEmojis[1]);

            Assert.IsNotNull(t.Hashtags);
            Assert.AreEqual(_hashtags.Count, t.Hashtags.Count);
            CollectionAssert.Contains(t.Hashtags, _hashtags[0]);
            CollectionAssert.Contains(t.Hashtags, _hashtags[1]);

            Assert.IsNotNull(t.Urls);
            Assert.AreEqual(_urls.Count, t.Urls.Count);
            Assert.IsTrue(t.Urls[0].Url == _urls[0] || t.Urls[0].Url == _urls[1]);
            Assert.IsTrue(t.Urls[1].Url == _urls[0] || t.Urls[1].Url == _urls[1]);

            return true;
        }

        [TestMethod]
        public void OnTweetReceived_UrlTranslation_Happy()
        {
            var destUrls = new[] { "https://some.other.place/somewhere1", "https://some.other.place/somewhere2" };
            var translationTable = new Dictionary<string, string>
            {
                { "1", destUrls[0] },
                { "2", destUrls[1] }
            };

            var tcoUrls = new List<string>();
            foreach (var (k, _) in translationTable)
            {
                tcoUrls.Add($"https://t.co/{k}");
            }

            var tweet = new Tweet
            {
                Id = 87654,
                Text = $" {tcoUrls[0]} {tcoUrls[1]} "
            };

            _statsServiceMock
               .Setup(x => x.TweetReceived(It.IsAny<Tweet>()));

            string tcoPath = null;
            _translationServiceMock
               .Setup(x => x.TranslateUrlAsync(It.IsAny<string>()))
               .Callback((string p) => tcoPath = p)
               .ReturnsAsync(() => translationTable[tcoPath]);

            New().OnTweetReceived(tweet);
        }

        [TestMethod]
        public void Dispose_Happy()
        {
            _streamHandlerMock
               .Setup(x => x.StopAsync())
               .Returns(Task.CompletedTask);

            New().Dispose();
        }

        private TwitterApiService New()
            => new TwitterApiService(
                _optionsMock.Object,
                _loggerFactoryMock.Object,
                _statsServiceMock.Object,
                _streamHandlerMock.Object,
                _translationServiceMock.Object);

        private class TestMessageHandler : HttpClientHandler
        {
            private readonly HttpResponseMessage _response;
            private readonly CancellationToken _canxToken;

            public TestMessageHandler(HttpResponseMessage response, CancellationToken ct)
            {
                _response = response;
                _canxToken = ct;
            }

            public bool IsDisposed { get; private set; }

            public HttpRequestMessage Request { get; private set; }

            protected override void Dispose(bool disposing)
            {
                IsDisposed = true;
                base.Dispose(disposing);
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
            {
                Request = req;

                return Task.FromResult(_response);
            }
        }
    }
}
