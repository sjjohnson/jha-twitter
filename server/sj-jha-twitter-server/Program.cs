using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace sj_jha_twitter_server
{
    public class Program
    {
        private const string EmojiResourceName = "sj_jha_twitter_server.emojis.txt";

        public static void Main(string[] args)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmojiResourceName);
            if (stream == null)
            {
                throw new Exception($"Embedded resource '{EmojiResourceName}' not found");
            }

            using var reader = new StreamReader(stream, Encoding.UTF8, false, -1, true);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    line = line.Replace(".png", "");
                    EmojiCatalog.AddName(line);
                }
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .AddEnvironmentVariables()
               .Build();

            return Host.CreateDefaultBuilder(args)
               .ConfigureWebHost(b => b.UseConfiguration(config))
               .ConfigureWebHostDefaults(b => b.UseStartup<Startup>());
        }
    }
}
