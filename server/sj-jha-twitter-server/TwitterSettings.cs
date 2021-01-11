using System;

namespace sj_jha_twitter_server
{
    public class TwitterSettings
    {
        public const string TwitterTokenEnvVar = "TWITTER_API_TOKEN";

        public string BaseUrl { get; set; }

        public string SampleStreamEndpoint { get; set; }

        public string TwitterApiToken => Environment.GetEnvironmentVariable("TWITTER_API_TOKEN");
    }
}
