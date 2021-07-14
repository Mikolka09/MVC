using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    //var localConfig = new Dictionary<string, string>
                    //{
                    //    ["CosmosDB:AccountEndpoint"] = "https://localhost:8081"
                    //};
                    //configuration.AddInMemoryCollection(localConfig);

                    configuration.AddUserSecrets<Startup>(true, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
