using System.Collections.Generic;
using Newtonsoft.Json;

namespace sj_jha_twitter_app
{
    internal class TwitterStats
    {
        [JsonProperty("top10Domains")]
        public List<string> Top10Domains { get; set; }

        [JsonProperty("top10Emojis")]
        public List<string> Top10Emojis { get; set; }

        [JsonProperty("top10Hashtags")]
        public List<string> Top10Hashtags { get; set; }

        [JsonProperty("tweetCount")]
        public long TweetCount { get; set; }

        [JsonProperty("tweetsPerSecond")]
        public double TweetsPerSecond { get; set; }

        [JsonProperty("tweetsWithEmojis")]
        public long TweetsWithEmojis { get; set; }

        [JsonProperty("tweetsWithPhotoUrls")]
        public long TweetsWithPhotoUrls { get; set; }

        [JsonProperty("tweetsWithUrls")]
        public long TweetsWithUrls { get; set; }
    }
}
