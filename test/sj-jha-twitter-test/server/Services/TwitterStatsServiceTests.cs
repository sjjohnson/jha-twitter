using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sj_jha_twitter_server.Services;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_test.server.Services
{
    [TestClass]
    public class TwitterStatsServiceTests
    {
        private const string TestTweetTextBase64 = "QFBob2thVFNWIEBLYWl6ZXJDaGllZnMgRXZlbiBpZiB3ZSBhcmUgbGVhZCBieSBQZXAgd2lsbCBzdGlsbCBiZSBsb29zaW5nLi4KIEtjIGdvdCBpbnRlcm5hbCBwcm9ibGVtcyDwn5ip8J+kpvCfj73igI3imYLvuI8=";

        private readonly Mock<ILoggerFactory> _loggerFactoryMock = new Mock<ILoggerFactory>(MockBehavior.Strict);
        private readonly string _testTweetText;

        public TwitterStatsServiceTests()
        {
            var bytes = Convert.FromBase64String(TestTweetTextBase64);

            _testTweetText = Encoding.UTF8.GetString(bytes);
        }

        private static string RndStr => $"{Guid.NewGuid():n}";

        [TestInitialize]
        public void TestInitialize()
            => _loggerFactoryMock
               .Setup(x => x.CreateLogger(typeof(TwitterStatsService).FullName))
               .Returns(new Mock<ILogger>(MockBehavior.Loose).Object);

        [TestMethod]
        public void Constructor_Happy()
        {
            Assert.IsNotNull(
                new TwitterStatsService(_loggerFactoryMock.Object));
        }

        [TestMethod]
        public void Constructor_NullLoggerFactory_ThrowsArgumentNullException()
        {
            var x = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterStatsService(null));

            Assert.AreEqual("loggerFactory", x.ParamName);
        }

        [TestMethod]
        public void RoundTrip_Happy()
        {
            var service = new TwitterStatsService(_loggerFactoryMock.Object);
            var emojis = new List<string> { RndStr, RndStr };
            var hashtags = new List<string> { $"#{RndStr}", $"#{RndStr}" };
            var domains = new List<string> { "instagram.com", "twitter.com" };
            var hosts = new List<string> { domains[0], $"pic.{domains[1]}"};
            var rawUrls = new List<string> { $"https://{hosts[0]}", $"https://pic.{hosts[1]}" };
            var urls = new List<UrlInfo>
            {
                new UrlInfo
                {
                    Host = hosts[0],
                    Left = rawUrls[0],
                    Path = null,
                    Url = rawUrls[0]
                },
                new UrlInfo
                {
                    Host = hosts[1],
                    Left = rawUrls[1],
                    Path = null,
                    Url = rawUrls[1]
                }
            };

            var tweet = new Tweet
            {
                Emojis = emojis,
                Hashtags = hashtags,
                Urls = urls,
                Id = 987,
                Text = _testTweetText,
            };

            service.TweetReceived(tweet);

            var result = service.GetStats();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.TweetCount);
            Assert.IsTrue(result.TweetsPerSecond > 0);
            Assert.AreEqual(1, result.TweetsWithEmojis);
            Assert.AreEqual(1, result.TweetsWithPhotoUrls);
            Assert.AreEqual(1, result.TweetsWithUrls);

            var top10Domains = result.Top10Domains.ToList();
            Assert.AreEqual(2, top10Domains.Count);
            CollectionAssert.Contains(top10Domains, domains[0]);
            CollectionAssert.Contains(top10Domains, domains[1]);
        }

        /*[TestMethod]
        public void GetStats_Happy()
        {
            var testStats = new TwitterStats();
            var service = New();

            service.Stats = testStats;

            var result = service.Stats;

            Assert.AreSame(testStats, result);
        }

        [TestMethod]
        public void SetStats_Null_ThrowsArgumentNullException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(
                () => New().Stats = null);

            Assert.AreEqual("value", ex.ParamName);
        }*/

        private ITwitterStatsService New()
            => new TwitterStatsService(_loggerFactoryMock.Object);
    }
}
