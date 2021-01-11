using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_test.server.Twitter
{
    [TestClass]
    public class TwitterStreamHandlerTests
    {
        const string StreamBase64 = "eyJkYXRhIjp7ImlkIjo4NzY1NCwidGV4dCI6ImVtb2ppU3RhcnRAUGhva2FUU1YgQEthaXplckNoaWVmcyBFdmVuIGlmIHdlIGFyZSBsZWFkIGJ5IFBlcCB3aWxsIHN0aWxsIGJlIGxvb3NpbmcuLlxuIEtjIGdvdCBpbnRlcm5hbCBwcm9ibGVtcyDwn5ip8J+kpvCfj73igI3imYLvuI9lbW9qaUVuZCB1cmxTdGFydGh0dHBzOi8vd3d3LjQ0YTJjYmI3ZDJlOTRlZTlhN2UwYjYwMTlkZGRmZWU0LmNvbSBodHRwczovL3d3dy4xODU0NGEyNTc4NDQ0NjJjYmZjODE2ODUxZTU0MDI3My5jb20gdXJsRW5kIGhhc2h0YWdTdGFydCAjMThmMjBmYjg0MDY5NDQyYTlmZTY0NDIyYWVhNmUwZDYgIzczYjZmZjZkY2Y2MTQ5MTM5MTA4ODZmMjNiNjU2NDRkIEVORCJ9fQ0KeyJkYXRhIjp7ImlkIjo4NzY1NCwidGV4dCI6ImVtb2ppU3RhcnRAUGhva2FUU1YgQEthaXplckNoaWVmcyBFdmVuIGlmIHdlIGFyZSBsZWFkIGJ5IFBlcCB3aWxsIHN0aWxsIGJlIGxvb3NpbmcuLlxuIEtjIGdvdCBpbnRlcm5hbCBwcm9ibGVtcyDwn5ip8J+kpvCfj73igI3imYLvuI9lbW9qaUVuZCB1cmxTdGFydGh0dHBzOi8vd3d3LjQ0YTJjYmI3ZDJlOTRlZTlhN2UwYjYwMTlkZGRmZWU0LmNvbSBodHRwczovL3d3dy4xODU0NGEyNTc4NDQ0NjJjYmZjODE2ODUxZTU0MDI3My5jb20gdXJsRW5kIGhhc2h0YWdTdGFydCAjMThmMjBmYjg0MDY5NDQyYTlmZTY0NDIyYWVhNmUwZDYgIzczYjZmZjZkY2Y2MTQ5MTM5MTA4ODZmMjNiNjU2NDRkIEVORCJ9fQ0K";

        private static readonly Random __rng = new Random(Environment.TickCount);

        private readonly byte[] _streamBytes;
        private readonly Mock<Stream> _streamMock = new Mock<Stream>(MockBehavior.Strict);
        private readonly Mock<ITweetHandler> _tweetHandlerMock = new Mock<ITweetHandler>(MockBehavior.Strict);

        public TwitterStreamHandlerTests()
            => _streamBytes = Convert.FromBase64String(StreamBase64);

        [TestMethod]
        public void Constructor_Happy()
        {
            Assert.IsNotNull(new TwitterStreamHandler());
        }

        [TestMethod]
        public async Task StartStopAsync_Happy()
        {
            int len = 0;

            _streamMock
               .SetupGet(x => x.CanRead)
               .Returns(true);
            _streamMock
               .Setup(x => x.ReadAsync(It.IsAny<Memory<byte>>(), It.IsAny<CancellationToken>()))
               .Callback(
                    (Memory<byte> m, CancellationToken t) =>
                    {
                        len = m.Length;
                        __rng.NextBytes(m.ToArray());
                    })
               .ReturnsAsync(len);

            var handler = new TwitterStreamHandler();

            await handler.StartAsync(_streamMock.Object, Encoding.UTF8, _tweetHandlerMock.Object);

            handler.LoopTask.Wait(50);

            await handler.StopAsync();
        }

        [TestMethod]
        public async Task ReadTweets_Happy()
        {
            var stream = new MemoryStream(_streamBytes);
            var tweetHandler = new TestTweetHandler();
            var streamHandler = new TwitterStreamHandler();

            await streamHandler.StartAsync(stream, Encoding.UTF8, tweetHandler);

            streamHandler.LoopTask.Wait(100);

            var stopTask = streamHandler.StopAsync();

            stopTask.Wait(100);

            Assert.AreEqual(2, tweetHandler.ReceivedTweets.Count);
        }

        private class TestTweetHandler : ITweetHandler
        {
            private readonly List<Tweet> _receivedTweets = new List<Tweet>();

            public List<Tweet> ReceivedTweets => _receivedTweets;

            public void OnTweetReceived(Tweet t)
            {
                _receivedTweets.Add(t);
            }
        }
    }
}
