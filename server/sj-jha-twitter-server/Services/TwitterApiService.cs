using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sj_jha_twitter_server.Twitter;

namespace sj_jha_twitter_server.Services
{
    public class TwitterApiService : IHostedService, ITweetHandler, IDisposable
    {
        // Adapted from: http://urlregex.com
        // Only works for http/s URLs
        private static readonly Regex __urlExp = new Regex(
            @"(?<left>http(s?)\:\/\/(?<host>[0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*)(:(0-9)*)*)(\/?)(?<path>[a-zA-Z0-9\-\.\?\,\'\/\+&%\$#_]*)?",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex __hashtagExp = new Regex(
            @"(\s|^)?(?<tag>#[a-z0-9\-\._]+)(\s|$)",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly List<string> __filterHosts = new List<string> { "t", "t.c", "t.co" };

        private static HttpClient __httpClient;

        private readonly TwitterSettings _settings;
        private readonly ILogger<TwitterApiService> _logger;
        private readonly ITwitterStatsService _statsService;
        private readonly IUrlTranslationService _urlTranslator;
        private ITwitterStreamHandler _streamHandler;

        public TwitterApiService(
            IOptions<TwitterSettings> options,
            ILoggerFactory loggerFactory,
            ITwitterStatsService statsService,
            ITwitterStreamHandler streamHandler,
            IUrlTranslationService urlTranslator)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            _ = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _statsService = statsService ?? throw new ArgumentNullException(nameof(statsService));
            _streamHandler = streamHandler ?? throw new ArgumentNullException(nameof(streamHandler));
            _urlTranslator = urlTranslator ?? throw new ArgumentNullException(nameof(urlTranslator));

            _settings = options.Value ?? throw new ArgumentException($"{nameof(options)}.Value returned null", $"{nameof(options)}");
            _logger = loggerFactory.CreateLogger<TwitterApiService>();
        }

        public HttpMessageHandler MessageHandler { get; set; } = new HttpClientHandler();

        private HttpClient HttpClient
        {
            get
            {
                if (__httpClient == null)
                {
                    __httpClient = new HttpClient(MessageHandler, true)
                    {
                        BaseAddress = new Uri(_settings.BaseUrl, UriKind.Absolute)
                    };
                    __httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_settings.TwitterApiToken}");
                    __httpClient.DefaultRequestHeaders.Add("User-Agent", "sj-jha-twitter-test");
                }
                return __httpClient;
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting TwitterApiService");

            await ConnectAsync(cancellationToken);

            _logger.LogInformation("TwitterApiService is running");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping TwitterApiService");

            await _streamHandler.StopAsync();

            _logger.LogInformation("TwitterApiService is stopped");
        }

        public void OnTweetReceived(Tweet t)
        {
            _logger.LogTrace($"Tweet received: {t.Text}");

            t.Emojis = GetTweetEmojis(t.Text);
            t.Hashtags = GetTweetHashtags(t.Text);
            t.Urls = GetTweetUrls(t.Text);

            _statsService.TweetReceived(t);
        }

        public void Dispose()
        {
            if (_streamHandler != null)
            {
                _streamHandler.StopAsync().Wait();
                _streamHandler = null;
            }
        }

        private async Task ConnectAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(" ** TwitterApiService: ENTER ConnectAsync");

            // TODO: Dispose?
            var response = await HttpClient.GetAsync(
                _settings.SampleStreamEndpoint,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            await _streamHandler.StartAsync(stream, Encoding.UTF8, this);

            _logger.LogInformation(" ** TwitterApiService: EXIT ConnectAsync");
        }

        private List<string> GetTweetEmojis(string tweetText)
        {
            var emojis = new List<string>();
            var list = new List<string>();
            var chars = tweetText.ToCharArray();

            var lastWasZwj = false;
            var lastWasJoinable = false;
            for (var i = 0; i < chars.Length; ++i)
            {
                var c = chars[i];
                if (c == 0x200d)
                {
                    lastWasJoinable = true;
                    lastWasZwj = true;
                    list.Add("200d");
                }
                else if (c == 0xfe0f)
                {
                    lastWasJoinable = true;
                    lastWasZwj = false;
                    list.Add("fe0f");
                }
                else if (char.GetUnicodeCategory(c) == UnicodeCategory.Surrogate)
                {
                    lastWasZwj = false;
                    if (char.IsHighSurrogate(c))
                    {
                        var utf32 = char.ConvertToUtf32(c, chars[i + 1]);

                        ++i;

                        lastWasJoinable = true;
                        list.Add($"{utf32:x}");
                    }
                }
                else
                {
                    if (lastWasZwj)
                    {
                        list.Add($"{(int)c:x}");
                    }
                    else
                    {
                        if (lastWasJoinable)
                        {
                            emojis.AddRange(ProcessTweetEmojis(list));
                        }
                        list.Clear();
                        lastWasJoinable = false;
                    }
                }
            }

            if (lastWasJoinable)
            {
                emojis.AddRange(ProcessTweetEmojis(list));
            }

            return emojis;
        }

        private List<string> ProcessTweetEmojis(List<string> codepoints)
        {
            var emojis = new List<string>();
            var emojiName = string.Join('-', codepoints);
            codepoints.Clear();

            while (true)
            {
                if (EmojiCatalog.Exists(emojiName))
                {
                    emojis.Add(emojiName);

                    if (codepoints.Count > 0)
                    {
                        emojiName = string.Join('-', codepoints);
                        codepoints.Clear();
                        continue;
                    }

                    break;
                }

                if (!emojiName.Contains('-'))
                {
                    break;
                }

                var parts = emojiName.Split('-', 2, StringSplitOptions.RemoveEmptyEntries);
                codepoints.Add(parts[0]);
                emojiName = parts[1];
            }

            return emojis;
        }

        private List<string> GetTweetHashtags(string tweetText)
        {
            var list = new List<string>();

            foreach (Match m in __hashtagExp.Matches(tweetText))
            {
                list.Add(m.Groups["tag"].Value);
            }

            return list;
        }

        private List<UrlInfo> GetTweetUrls(string tweetText)
        {
            var list = new List<UrlInfo>();

            var translateTasks = new List<Task>();

            foreach (Match m in __urlExp.Matches(tweetText))
            {
                var info = new UrlInfo
                {
                    Host = m.Groups["host"].Value,
                    Left = m.Groups["left"].Value,
                    Path = m.Groups["path"].Value,
                    Url = m.Value
                };

                list.Add(info);

                if (info.Host == "t.co")
                {
                    translateTasks.Add(Task.Run(async () => await TranslateUrlAsync(info)));
                }
            }

            if (translateTasks.Count > 0)
            {
                Task.WhenAll(translateTasks).Wait();
            }

            var toRemove = new List<int>();
            for (var i = 0; i < list.Count; ++i)
            {
                if (__filterHosts.Contains(list[i].Host))
                {
                    toRemove.Add(i);
                }
            }

            if (toRemove.Count > 0)
            {
                toRemove.Sort((p1, p2) => p2.CompareTo(p1));

                foreach (var i in toRemove)
                {
                    list.RemoveAt(i);
                }
            }

            return list;
        }

        private async Task TranslateUrlAsync(UrlInfo info)
        {
            var url = await _urlTranslator.TranslateUrlAsync(info.Path);

            if (url != null)
            {
                var m = __urlExp.Match(url);
                if (m.Success)
                {
                    info.Host = m.Groups["host"].Value;
                    info.Left = m.Groups["path"].Value;
                    info.Path = m.Groups["path"].Value;
                    info.Url = url;
                }
            }
        }
    }
}
