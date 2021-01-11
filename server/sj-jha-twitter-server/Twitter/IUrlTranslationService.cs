using System.Threading.Tasks;

namespace sj_jha_twitter_server.Twitter
{
    public interface IUrlTranslationService
    {
        Task<string> TranslateUrlAsync(string twitterPath);
    }
}
