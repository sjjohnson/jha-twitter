using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_server.Services
{
    public interface ITwitterStatsService
    {
        void TweetReceived(Tweet t);

        TwitterStats GetStats();
    }
}
