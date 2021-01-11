namespace sj_jha_twitter_server.Twitter
{
    public interface ITweetHandler
    {
        void OnTweetReceived(Tweet t);
    }
}
