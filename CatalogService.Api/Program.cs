using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CatalogService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Run();
        }

        public static IWebHost CreateHostBuilder(string[] args)
        {
            return  WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().UseContentRoot(Directory.GetCurrentDirectory()).Build();
        }
    }


    
}
