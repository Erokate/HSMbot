using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Runtime;

namespace HSMbot
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }

        //private static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureHostConfiguration(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });


    }
}
