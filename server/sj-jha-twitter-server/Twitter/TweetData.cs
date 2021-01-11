using Newtonsoft.Json;

namespace sj_jha_twitter_server.Twitter
{
    public class TweetData
    {
        [JsonProperty("data")]
        public Tweet Data { get; set; }
    }
}
