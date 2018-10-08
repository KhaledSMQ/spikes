using System;
using System.Reactive.Subjects;

namespace RxExperiments
{
	public class EventsVsObservables
	{
		private static event Action<int> ChangedViaEvent;
		private static Subject<int> ChangedViaSubject = new Subject<int>();

		public void Run()
		{
			ChangedViaEvent += OnChangedViaEvent;

			var handler = ChangedViaEvent;
			if (handler != null)
				handler(42);

			ChangedViaEvent -= OnChangedViaEvent;
	
			var subscriber = ChangedViaSubject.Subscribe(x =>
			                         {
				                         Console.WriteLine(x);
			                         });


			ChangedViaSubject.OnNext(43);
			subscriber.Dispose();
		}

		private void OnChangedViaEvent(int i)
		{
			Console.WriteLine(i);
		}
	}
}
