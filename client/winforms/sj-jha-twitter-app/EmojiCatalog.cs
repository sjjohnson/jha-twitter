using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace sj_jha_twitter_app
{
    public static class EmojiCatalog
    {
        private const string StreamNameFormat = "sj_jha_twitter_app.emojis.{0}.png"; // "svg";

        private static readonly HashSet<string> __emojis = new HashSet<string>();

        public static void AddName(string name) => __emojis.Add(name);

        public static Stream GetStream(string name)
        {
            var streamName = string.Format(StreamNameFormat, name);
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(streamName);
            if (stream != null)
            {
                return stream;
            }
            throw new FileNotFoundException("Resource not found", streamName);
        }
    }
}
