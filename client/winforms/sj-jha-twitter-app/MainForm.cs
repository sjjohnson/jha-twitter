using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Config = System.Configuration.ConfigurationManager;

namespace sj_jha_twitter_app
{
    public partial class MainForm : Form
    {
        private string _serverUrl;
        private int _serverPort = 80;
        private int _statCheckIntervalInSeconds = 5;

        private CancellationTokenSource _cts;
        private Task _statsTask;

        private PictureBox[] _pics;

        public MainForm()
        {
            InitializeComponent();
        }

        private static void IgnoreCancel(Action action)
        {
            try
            {
                action();
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                //Ignore
            }
            catch (TaskCanceledException)
            {
                // Ignore
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SafeDo(
                () =>
                {
                    _serverUrl = Config.AppSettings["serverUrl"];
                    var portConfig = Config.AppSettings["serverPort"];
                    var intervalConfig = Config.AppSettings["statCheckIntervalInSeconds"];

                    if (!string.IsNullOrWhiteSpace(portConfig))
                    {
                        _serverPort = int.Parse(portConfig);
                    }

                    if (!string.IsNullOrWhiteSpace(intervalConfig))
                    {
                        _statCheckIntervalInSeconds = int.Parse(intervalConfig);
                    }

                    _pics = new[] { emoji1, emoji2, emoji3, emoji4, emoji5, emoji6, emoji7, emoji8, emoji9, emoji10 };
                });
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SafeDo(
                () =>
                {
                    startButton.Enabled = false;

                    _cts = new CancellationTokenSource();

                    _statsTask = Task.Run(async () => await StatsLoopAsync(_cts.Token));

                    stopButton.Enabled = true;
                });
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            SafeDo(
                () =>
                {
                    stopButton.Enabled = false;

                    _cts.Cancel();

                    IgnoreCancel(() => _statsTask.Wait());

                    _cts = null;

                    _statsTask = null;

                    startButton.Enabled = true;
                });
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            SafeDo(Close);
        }

        private void ShowException(Exception ex)
        {
            MessageBox.Show(this, ex.ToString(), ex.Message);
        }

        private void SafeDo(Action op)
        {
            try
            {
                op();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private async Task StatsLoopAsync(CancellationToken cx)
        {
            var port = _serverPort != 80 ? $":{_serverPort}" : string.Empty;
            var url = $"{_serverUrl.Trim('/')}{port}";

            using (var http = new HttpClient { BaseAddress = new Uri(url, UriKind.Absolute) })
            {
                while (!cx.IsCancellationRequested)
                {
                    try
                    {
                        string statsJson;
                        using (var response = await http.GetAsync("/twitterstats", cx))
                        {
                            response.EnsureSuccessStatusCode();

                            statsJson = await response.Content.ReadAsStringAsync();
                        }

                        var stats = JsonConvert.DeserializeObject<TwitterStats>(statsJson);

                        DoInvoke(() => ProcessStatsJson(stats));
                    }
                    catch (Exception ex)
                    {
                        ShowException(ex);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(_statCheckIntervalInSeconds), cx);
                    if (cx.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
        }

        private void ProcessStatsJson(TwitterStats stats)
        {
            string ToPercentString(long tweetCount, long checkCount)
            {
                var pct = checkCount * 100 / (double)tweetCount;

                return $"{pct:n1}";
            }

            tweetCountLabel.Text = $"{stats.TweetCount:n0}";
            tweetsPerSecLabel.Text = $"{stats.TweetsPerSecond:n1}";
            emojiPercentLabel.Text = ToPercentString(stats.TweetCount, stats.TweetsWithEmojis);
            urlPercentLabel.Text = ToPercentString(stats.TweetCount, stats.TweetsWithUrls);
            photoUrlPercentLabel.Text = ToPercentString(stats.TweetCount, stats.TweetsWithPhotoUrls);
            topDomainsTextBox.Text = string.Join("\r\n", stats.Top10Domains);
            topHashtagsTextBox.Text = string.Join("\r\n", stats.Top10Hashtags);

            SetEmojis(stats.Top10Emojis);
        }

        private void SetEmojis(List<string> emojis)
        {
            var count = Math.Min(_pics.Length, emojis.Count);

            int i;
            for (i = 0; i < count; ++i)
            {
                try
                {
                    var img = Image.FromStream(EmojiCatalog.GetStream(emojis[i]));

                    // TODO: Fix memory leak
                    _pics[i].Image = img;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
            if (i < count)
            {
                for (; i < count; ++i)
                {
                    _pics[i].Image = null;
                }
            }
        }

        private void DoInvoke(Action action) => Invoke(action);
    }
}

// Zero-Width Joiner (ZWJ)
// HighSur / LowSur (U+200D) 

// Skin tone modifier
// 1f3fb - 1f3ff

// Variation Selector 16
// FE0F
