using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global
namespace sj_jha_twitter_server.Twitter
{
    public class TwitterStreamHandler : ITwitterStreamHandler
    {
        private readonly CancellationTokenSource _cts;

        private ITweetHandler _tweetHandler;
        private Stream _stream;
        private Encoding _encoding;

        public TwitterStreamHandler() => _cts = new CancellationTokenSource();

        public Task LoopTask { get; private set; }

        public Task StartAsync(Stream stream, Encoding encoding, ITweetHandler tweetHandler)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            _tweetHandler = tweetHandler ?? throw new ArgumentNullException(nameof(tweetHandler));

            LoopTask = Task.Run(async () => await DeserializationLoopAsync(_cts.Token));
            return Task.CompletedTask;
        }

        public async Task StopAsync()
        {
            _cts.Cancel();

            if (LoopTask  != null)
            {
                await LoopTask;

                LoopTask = null;
            }
        }

        private async Task DeserializationLoopAsync(CancellationToken ct)
        {
            var accumulator = new StringBuilder();
            var buffer = new char[1024];
            var bufferMem = buffer.AsMemory();
            using var sr = new StreamReader(_stream, _encoding, false, buffer.Length, true);

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var count =  await sr.ReadAsync(bufferMem, ct);
                    for (var i = 0; i < count; ++i)
                    {
                        if (buffer[i] == '\r' || buffer[i] == '\n')
                        {
                            if (accumulator.Length > 0)
                            {
                                try
                                {
                                    var tweet = JsonConvert.DeserializeObject<TweetData>(accumulator.ToString());

                                    _tweetHandler.OnTweetReceived(tweet.Data);

                                    accumulator.Clear();
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"{ex}");
                                }
                            }

                            while (i < count && buffer[i] == '\r' || buffer[i] == '\n')
                            {
                                ++i;
                            }

                            continue;
                        }

                        accumulator.Append(buffer[i]);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // Intentionally empty
            }
        }
    }
}
