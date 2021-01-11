using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sj_jha_twitter_app
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();            
            foreach (var name in resources)
            {
                var m = Regex.Match(name, @"\.emojis\.(?<name>[^.]+)\.\w\w\w");
                if (m.Success)
                {
                    EmojiCatalog.AddName(m.Groups["name"].Value);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
