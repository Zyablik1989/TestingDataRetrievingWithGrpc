using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingDataService;
using Grpc.Core;

namespace TestingDataProcessor.Services
{
    public class TestingDataHandler : TestingDataRetriever.TestingDataRetrieverBase
    {
        public override Task<TestingData> GetTestingData(ClientCredentials client, ServerCallContext context)
        {
            return Task.FromResult(ProcessTestingData(client.ClientType));
        }

        private TestingData ProcessTestingData(ClientType clientType)
        {
            var testingData = new TestingData();
            var levelOfDataDetalisation = 0;
            switch (clientType)
            {
                default:
                    testingData.Comment = "ОШИБКА ДОЛЖНОСТИ";
                    break;
                case ClientType.JuniorResearcher:
                    testingData.SignalType = SignalType.Oss;
                    levelOfDataDetalisation = 4;
                    break;
                case ClientType.Researcher:
                    testingData.SignalType = SignalType.Aws;
                    levelOfDataDetalisation = 80;
                    break;
                case ClientType.SeniorResearcher:
                    testingData.SignalType = SignalType.Murf;
                    levelOfDataDetalisation = 500;
                    break;
            }

            if (string.IsNullOrEmpty(testingData.Comment))
            {
                testingData.Lambda = (uint)new Random().Next(0,int.MaxValue);
                testingData.Frequency = (uint)new Random().Next(0, int.MaxValue);
                testingData.Data = new string(Enumerable.Repeat("0123456789", levelOfDataDetalisation)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());
            }

            return testingData;
        }
    }


}
