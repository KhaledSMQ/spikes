using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpikesCo.Platform.Messaging;
using SpikesCo.Platform.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Setup();
            var t = p.Send();
            while (true)
            {
                
            }
        }

        private const string Path = "INT.T.RTBRIDGE.publish";
        private const string ConnectionString = "Endpoint=sb://gensb-rt-dev.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=qRnsn7m8fyVuMLXkpjLS85o2LAcAPWTjO2nw6F33HsA=";
        private const int LogSize = 10;

        private TopicClient Client { get; set; }
        private SubscriptionClient SubscriptionClient { get; set; }

        public void Setup()
        {
            Client = TopicClient.CreateFromConnectionString(ConnectionString, Path);
            SubscriptionClient =
                SubscriptionClient.CreateFromConnectionString(
                    "Endpoint=sb://gensb-rt-dev.servicebus.windows.net/;SharedAccessKeyName=Listen;SharedAccessKey=2PBClMF5iwCyewaxGia84zpnvAIt9lCgwC1jx8ivObc=",
                    Path, "persistence2");
            SubscriptionClient.OnMessage(MessageReceived);
        }

        public async Task Send()
        {
            foreach (var i in Enumerable.Range(1, 20))
            {
                var message = new Message<string>("PlatformEvent", TestAlert.Replace("Test Alert", "Test Alert " + i + " - " + DateTime.UtcNow));
                using (var bm = message.ToBrokeredMessage())
                {
                    try
                    {
                        await Client.SendAsync(bm);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }

        private int _counter;
        private int _pevcounter;

        private void MessageReceived(BrokeredMessage bm)
        {
            bm.Complete();
            var contentType = bm.ContentType;

            Interlocked.Increment(ref _counter);

            if (_counter % 100 == 1)
                Console.WriteLine(_counter);

            if (contentType == "PlatformEvent")
            {
                var body = bm.GetBody<string>();
                var pos = body.IndexOf("Test Alert");
                if (pos > -1)
                {
                    Interlocked.Increment(ref _pevcounter);
                    Console.WriteLine("PEV " + _pevcounter + ": " + body.Substring(pos, 13));
                }
                //Console.WriteLine(body);
                //Console.ReadLine();
            }
        }

        private const string TestAlert = @"{""Id"":0,""EventDetectionInstanceTypeId"":83395810,""EventDetectionInstanceId"":5720590,""EventDetectionTimestamp"":""2016-05-06T14:56:00+00:00"",""EffectiveStartTimestamp"":""2016-05-06T14:56:00+00:00"",""EffectiveEndTimestamp"":null,""Summary"":""Test Alert"",""Message"":""SpikeCo Alert: Output rose above 100 MW"",""AffectedEntities"":[],""ExtendedAttributes"":{""event-detection-rule-id"":2196,""event-detection-direction"":""Up""},""TriggeringCollectionInstanceDataStreamAssociations"":[{""DataStreamIdentifier"":{""GlobalEntityId"":80578640,""DataItemId"":50001,""CollectionInstanceTypeId"":83395782,""DataItemUniverseId"":0},""CollectionInstanceIdentifier"":{""CollectionInstanceTypeId"":83395782,""Id"":1102921532,""IdString"":null}}]}";
    }
}
