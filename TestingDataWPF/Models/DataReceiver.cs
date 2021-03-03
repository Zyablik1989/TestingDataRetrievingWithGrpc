using System;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using TestingDataService;

namespace TestingDataWPF.Models
{
    public class DataReceiver
    {
        static TestingData testingData= null;
        
        /// <summary>
        /// Trying to connect to server and receive data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        public static async Task ConnectToServer(string name, int position)
        {
            TestingData data = null;

            //recollecting connection data from settings
            var host = Properties.Settings.Default.TestingDataHost;
            var port = Properties.Settings.Default.TestingDataPort;

            try
            {
                //Turning TLS authorization off
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                using var channel = GrpcChannel.ForAddress($"{host}:{port}");
                var client = new TestingDataRetriever.TestingDataRetrieverClient(channel);

                //Async connection
                testingData = await client.GetTestingDataAsync(
                    new ClientCredentials()
                    {
                        Name = name,
                        ClientType = (ClientType) position + 1
                    });

                testingData.Comment =
                    new StringBuilder()
                        .Append("Testing Data was presented for ")
                        .Append(name)
                        .Append(" at ")
                        .Append(DateTime.Now.ToString())
                        .ToString();

            }
            //If this is gRPC error, we know how to present it to user
            catch (RpcException ex)
            {
                testingData = new TestingData() { Comment = ex.Message };
                ex.Data.Add("UserMessage", $"An error occurred when testing data was requested from {host}:{port}. RPC:" + ((StatusCode)(ex.StatusCode)));
                throw ex;
            }
            catch (Exception e)
            {
                testingData = new TestingData() {Comment = e.Message};
            }
        }

        public static async Task<TestingData> CollectTestingData(string name, int position)
        {
            testingData = null;
            await ConnectToServer(name, position);
            return testingData;
        }
        
    }
}
