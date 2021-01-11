using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_server.Services
{
    public class TwitterStatsService : ITwitterStatsService
    {
        private readonly ILogger<TwitterStatsService> _logger;

        private TwitterStats _stats;

        public TwitterStatsService(ILoggerFactory loggerFactory)
        {
            _ = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));

            _logger = loggerFactory.CreateLogger<TwitterStatsService>();
            _logger.LogTrace($"{nameof(TwitterStatsService)}.{MethodBase.GetCurrentMethod().Name}");
        }

        public void TweetReceived(Tweet t)
        {
            _stats = null;

            TwitterStats.AddTweet(t);
        }

        public TwitterStats GetStats()
        {
            _logger.LogTrace($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

            return _stats ??= TwitterStats.Latest;
        }
    }
}
