using System;
using System.ServiceModel.Dispatcher;

namespace Client
{
	public class ErrorHandler : IErrorHandler
	{
		public bool HandleError(Exception error)
		{
			Console.WriteLine(error);
			return false;
		}

		public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
		{
		}
	}
}
