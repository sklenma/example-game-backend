using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Example_Service.Definitions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Example_API
{
    public class Program
    {
        public static Definitions gd => DefinitionLoader.GameDefinitions;

        public static void Main(string[] args)
        {
            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName;
            //sets path for loading definitions
            gd.ResourcePath = Path.Combine(projectDirectory, "Example-Service",  "Resources", "");
            //Load definition
            gd.LoadDefinitions();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
