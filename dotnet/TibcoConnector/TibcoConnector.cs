using System;
using TIBCO.EMS;

namespace TibcoSample
{
    public class TibcoConnector
    {
        MessageConsumer _consumer;
        Connection _connection;
        Session _session;

		MyMessageListener Listener { get; set; }

		// Settings for Power IR updates
		private const string Url = "tcp://tsmain01.spikesco.com:10550, tcp://tsmain02.spikesco.com:10550";
		private const string UserName = "basicLookup";
		private const string Password = "b4s1cCr3ds";
		private const string Selector = "";
		private const string Topic = "tmp.t.ir";

		// Settings for Power RT updates
		//private const string Url = "tcp://tsclient01.spikesco.com:10560,tcp://tsclient02.spikesco.com:10560";
		//private const string UserName = "PowerRTUser";
		//private const string Password = "p0w3ru53r";
		//private const string Selector = "Pole='NA'";
		//private const string Topic = "int.t.to.pwr.publish";

		// Settings for FE updates
		//private const string Url = "tcp://dvfemsg01.spikesco.com:10590, tcp://dvfemsg02.spikesco.com:10590";
		//private const string UserName = "FEUser";
		//private const string Password = "@Bg2?Wa34!1";
		//private const string Selector = "";//measurementType = 'fe_measurement'";
		//private const string Topic = "int.t.fe.txt";

	    public void Init()
        {
            var factory = new ConnectionFactory(Url);
            Listener = new MyMessageListener();

            _connection = factory.CreateConnection(UserName, Password);
            _session = _connection.CreateSession(false, Session.AUTO_ACKNOWLEDGE);

            _consumer = _session.CreateConsumer(_session.CreateTopic(Topic), Selector);

            _consumer.MessageListener = Listener;
			//_consumer.MessageHandler += _consumer_MessageHandler;
        }

		void _consumer_MessageHandler(object sender, EMSMessageEventArgs args)
		{
		}

        public void Start()
        {
            _connection.Start();
        }

        public void Stop()
        {
	        var start = Listener.Start;
	        var end = DateTime.UtcNow;
	        var seconds = (end - start).Seconds;
	        var count = MyMessageListener.Counter;
	        var rate = 1.0 * count/seconds;

			Console.WriteLine("Received {0} messages in {1} seconds. Rate is {2} msgs/sec.", count, seconds, rate);

            _consumer.Close();
            _session.Close();
            _connection.Stop();
        }
    }
}
