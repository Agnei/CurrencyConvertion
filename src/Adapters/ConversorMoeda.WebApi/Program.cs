using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ConversorMoeda.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
            .CreateDefaultBuilder(args)
            //.UseHealthChecks("/hc")
            .UseStartup<Startup>()
            .UseUrls("http://localhost:5001/");
    }
}
