using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace sj_jha_twitter_server.Twitter
{
    public interface ITwitterStreamHandler
    {
        Task StartAsync(Stream stream, Encoding encoding, ITweetHandler tweetHandler);

        Task StopAsync();
    }
}
