using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using TestingDataService;

namespace TestingDataWPF
{
    public class DataRetriever
    {
        static TestingData testingData= null;
        
        /// <summary>
        /// Trying to connect to server and retrieve data
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        public static  void ConnectToServer(string name, int position)
        {
            TestingData data = null;
            try
            {
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new TestingDataRetriever.TestingDataRetrieverClient(channel);

                //While it's a test logic, connection is not asynchronous
                testingData =  client.GetTestingData(
                    new ClientCredentials()
                    {
                        Name = name,
                        ClientType = (ClientType) position+1
                    });
            }
            catch (Exception e)
            {
                testingData = new TestingData() {Comment = e.Message};
            }
        }

        public static TestingData CollectTestingData(string name, int position)
        {
            testingData = null;
             ConnectToServer(name, position);
            return testingData;
        }
    }
}
