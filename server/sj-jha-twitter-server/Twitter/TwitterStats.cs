using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace sj_jha_twitter_server.Twitter
{
    public class TwitterStats
    {
        private static readonly ConcurrentDictionary<string, long> __domains = new ConcurrentDictionary<string, long>();
        private static readonly ConcurrentDictionary<string, long> __emojis = new ConcurrentDictionary<string, long>();
        private static readonly ConcurrentDictionary<string, long> __hashtags = new ConcurrentDictionary<string, long>();
        private static readonly DateTime __startTime;

        private static long __tweetCount;
        private static long __tweetsWithEmojis;
        private static long __tweetsWithPhotoUrls;
        private static long __tweetsWithUrls;

        static TwitterStats()
            => __startTime = DateTime.UtcNow;

        public static TwitterStats Latest
        {
            get
            {
                var stats = new TwitterStats();

                return stats;
            }
        }

        [JsonProperty("top10Domains")]
        public IEnumerable<string> Top10Domains => GetTop10(__domains);

        [JsonProperty("top10Emojis")]
        public IEnumerable<string> Top10Emojis => GetTop10(__emojis);

        [JsonProperty("top10Hashtags")]
        public IEnumerable<string> Top10Hashtags => GetTop10(__hashtags);

        [JsonProperty("tweetCount")]
        public long TweetCount => __tweetCount;

        [JsonProperty("tweetsPerSecond")]
        public double TweetsPerSecond
            => __tweetCount / (DateTime.UtcNow - __startTime).TotalSeconds;

        [JsonProperty("tweetsWithEmojis")]
        public long TweetsWithEmojis => __tweetsWithEmojis;

        [JsonProperty("tweetsWithPhotoUrls")]
        public long TweetsWithPhotoUrls => __tweetsWithPhotoUrls;

        [JsonProperty("tweetsWithUrls")]
        public long TweetsWithUrls => __tweetsWithUrls;

        public static void AddTweet(Tweet t)
        {
            Interlocked.Increment(ref __tweetCount);

            if (t.Emojis?.Count > 0)
            {
                Interlocked.Increment(ref __tweetsWithEmojis);

                foreach (var e in t.Emojis)
                {
                    __emojis.AddOrUpdate(e, 1, (k, v) => v + 1);
                }
            }

            if (t.Hashtags?.Count > 0)
            {
                foreach (var h in t.Hashtags)
                {
                    __hashtags.AddOrUpdate(h, 1, (k, v) => v + 1);
                }
            }

            if (t.Urls?.Count > 0)
            {
                Interlocked.Increment(ref __tweetsWithUrls);

                var incrementedPhotos = false;
                foreach (var u in t.Urls)
                {
                    var host = u.Host.ToLower();
                    var parts = host.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    var domain = parts.Length > 1 ? $"{parts[^2]}.{parts[^1]}" : host;

                    __domains.AddOrUpdate(domain, 1, (k, v) => v + 1);

                    if (!incrementedPhotos && (host.Contains("pic.twitter.com") || host.Contains("instagram.com")))
                    {
                        Interlocked.Increment(ref __tweetsWithPhotoUrls);
                        incrementedPhotos = true;
                    }
                }
            }
        }

        private static IEnumerable<string> GetTop10(ConcurrentDictionary<string, long> source)
        {
            List<KeyValuePair<string, long>> pairs = null;
            Exception lastEx = null;
            for (var i = 0; i < 100; ++i)
            {
                try
                {
                    pairs = source.ToList();

                    break;
                }
                catch(Exception ex)
                {
                    lastEx = ex;
                }
            }

            if (pairs == null)
            {
                throw lastEx;
            }

            pairs.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));

            return pairs.Select(p => p.Key).Take(10);
        }
    }
}
