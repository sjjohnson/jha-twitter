using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sj_jha_twitter_server.Controllers;
using sj_jha_twitter_server.Services;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_test.server.Controllers
{
    [TestClass]
    public class TwitterStatsControllerTests
    {
        private readonly Mock<ITwitterStatsService> _statsServiceMock = new Mock<ITwitterStatsService>(MockBehavior.Strict);
        private readonly Mock<ILoggerFactory> _loggerFactoryMock = new Mock<ILoggerFactory>(MockBehavior.Strict);

        [TestInitialize]
        public void TestInitialize()
            => _loggerFactoryMock
               .Setup(x => x.CreateLogger(typeof(TwitterStatsController).FullName))
               .Returns(new Mock<ILogger>(MockBehavior.Loose).Object);

        [TestMethod]
        public void Constructor_Happy()
        {
            Assert.IsNotNull(
                new TwitterStatsController(_statsServiceMock.Object, _loggerFactoryMock.Object));
        }

        [TestMethod]
        public void Constructor_NullStatsService_ThrowsArgumentNullException()
        {
            var x = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterStatsController(null, _loggerFactoryMock.Object));

            Assert.AreEqual("statsService", x.ParamName);
        }

        [TestMethod]
        public void Constructor_NullLoggerFactory_ThrowsArgumentNullException()
        {
            var x = Assert.ThrowsException<ArgumentNullException>(
                () => new TwitterStatsController(_statsServiceMock.Object, null));

            Assert.AreEqual("loggerFactory", x.ParamName);
        }

        [TestMethod]
        public void Get_Happy()
        {
            var testStats = new TwitterStats();

            _statsServiceMock
               .Setup(x => x.GetStats())
               .Returns(testStats);

            var result = New().Get();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var ok = (OkObjectResult)result;

            Assert.AreSame(testStats, ok.Value);
        }

        [TestMethod]
        public void Get_Exception_Returns_500()
        {
            var testException = new Exception();

            _statsServiceMock
               .Setup(x => x.GetStats())
               .Throws(testException);

            var result = New().Get();

            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var code = (StatusCodeResult)result;

            Assert.AreEqual(500, code.StatusCode);
        }

        private TwitterStatsController New()
            => new TwitterStatsController(_statsServiceMock.Object, _loggerFactoryMock.Object);
    }
}
