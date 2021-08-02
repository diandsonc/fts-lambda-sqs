using Microsoft.Extensions.Configuration;

namespace Lambda.Core
{
    public class StartupUtil
    {
        private static string BASEPATH = System.IO.Directory.GetCurrentDirectory();

        public static IConfigurationBuilder Startup() => 
            new ConfigurationBuilder()
                .SetBasePath(BASEPATH)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();
    }
}