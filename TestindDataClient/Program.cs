using System;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using TestingDataService;


namespace TestindDataClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new TestingDataService.TestingDataRetriever.TestingDataRetrieverClient(channel);
            Console.WriteLine("Имя");
            var a = Console.ReadLine();
            Console.WriteLine("Должность 1-3");
            var b = Console.ReadLine();
            var position = 0;
            int.TryParse(b, out position);

            var data = await client.GetTestingDataAsync(
                new ClientCredentials() {

                Name = a,
                ClientType = (ClientType)position
            });

            Console.WriteLine(
                new StringBuilder()
                    .Append($"Lambda - {data.Lambda}{Environment.NewLine}")
                    .Append($"Frequency - {data.Frequency}{Environment.NewLine}")
                    .Append($"Data - {data.Data}{Environment.NewLine}")
                    .Append($"Comment - {data.Comment}{Environment.NewLine}")
                    .ToString());
            Console.ReadKey();
        }
    }
}
