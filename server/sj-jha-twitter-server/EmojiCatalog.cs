using System.Collections.Generic;

namespace sj_jha_twitter_server
{
    public static class EmojiCatalog
    {
        private static readonly HashSet<string> __names = new HashSet<string>();

        public static void AddName(string name) => __names.Add(name);

        public static bool Exists(string name) => __names.Contains(name);
    }
}
