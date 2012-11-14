using System;

namespace Infrastructure
{
	public static class MainEvents
	{
		public static event EventHandler AddFromOne;
		public static event EventHandler AddFromTwo;

		public static void RaiseAddFromOne()
		{
			var handler = AddFromOne;
			if (handler != null)
				handler(null, EventArgs.Empty);
		}

		public static void RaiseAddFromTwo()
		{
			var handler = AddFromTwo;
			if (handler != null)
				handler(null, EventArgs.Empty);
		}
	}
}
