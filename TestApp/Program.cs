using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NeilRamsbottom.Zaptec.Charger.Api;

namespace TestApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var username = configuration["ZaptecApi:Authentication:Username"];
            var password = configuration["ZaptecApi:Authentication:Password"];
            var authService = new ZaptecApiClient(
                new Uri(configuration["ZaptecApi:BaseUrl"]),
                username,
                password
            );

            var authToken = await authService.Authenticate();

            if (!authService.IsAuthenticated)
            {
                Console.WriteLine("Failed to authenticate.");
                Environment.ExitCode = 1;
                return;
            }

            var chargerState = await authService.GetChargerState("your-id-here");



        }
    }
}
