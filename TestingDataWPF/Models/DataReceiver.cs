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
            try
            {
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
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
                        .Append("Testing Data presented for ")
                        .Append(name)
                        .Append(" at ")
                        .Append(DateTime.Now.ToString())
                        .ToString();

            }
            //If this is gRPC error, we know how to present it to user
            catch (RpcException ex)
            {
                testingData = new TestingData() { Comment = ex.Message };
                ex.Data.Add("UserMessage", "An error occurred when testing data was requested. RPC:" + ((StatusCode)(ex.StatusCode)));
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
