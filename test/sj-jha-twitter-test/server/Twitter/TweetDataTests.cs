using Microsoft.VisualStudio.TestTools.UnitTesting;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_test.server.Twitter
{
    [TestClass]
    public class TweetDataTests
    {
        [TestMethod]
        public void Data_RoundTrip()
        {
            var tweet = new Tweet();

            var td = new TweetData();

            Assert.IsNull(td.Data);

            td.Data = tweet;

            Assert.AreSame(tweet, td.Data);
        }
    }
}
