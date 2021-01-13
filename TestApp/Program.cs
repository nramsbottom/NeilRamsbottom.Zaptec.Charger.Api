using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NeilRamsbottom.Zaptec.Charger.Api;
using NeilRamsbottom.Zaptec.Charger.Api.Models;

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

            var chargerId = "f262795a-e1a9-4797-adbc-4b17cb6db7ad";
            /*
            var chargerState = await authService.GetChargerState(chargerId);

            foreach (var item in chargerState)
            {
                Console.WriteLine(item.ChargerId);
                Console.WriteLine(item.StateId);
                Console.WriteLine(item.Timestamp);
                Console.WriteLine(item.Value);
                Console.WriteLine();
            }
            */

            var chargingHistory = await authService.GetZaptecChargingSessionsAsync(chargerId);


        }
    }
}
