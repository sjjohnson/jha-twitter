using System.Collections.Generic;
using Newtonsoft.Json;

namespace sj_jha_twitter_server.Twitter
{
    public class Tweet
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonIgnore]
        public List<string> Emojis { get; set; }

        [JsonIgnore]
        public List<string> Hashtags { get; set; }

        [JsonIgnore]
        public List<UrlInfo> Urls { get; set; }
    }
}
