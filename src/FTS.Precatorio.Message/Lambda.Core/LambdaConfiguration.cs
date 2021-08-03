using System.IO;
using Microsoft.Extensions.Configuration;

namespace Lambda.Core
{
    public class LambdaConfiguration
    {
        public static IConfiguration Configuration => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }
}