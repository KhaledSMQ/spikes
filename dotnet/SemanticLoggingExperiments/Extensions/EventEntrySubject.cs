using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace SemanticLoggingExperiments.Extensions
{
    internal sealed class EventEntrySubject : IObservable<EventEntry>, IObserver<EventEntry>, IDisposable
    {
        private readonly object lockObject = new object();
        private volatile ReadOnlyCollection<IObserver<EventEntry>> observers = new List<IObserver<EventEntry>>().AsReadOnly();
        private volatile bool isFrozen = false;

        public void Dispose()
        {
            this.OnCompleted();
        }

        public IDisposable Subscribe(IObserver<EventEntry> observer)
        {
            Guard.ArgumentNotNull(observer, "observer");

            lock (this.lockObject)
            {
                if (!this.isFrozen)
                {
                    var copy = this.observers.ToList();
                    copy.Add(observer);
                    this.observers = copy.AsReadOnly();
                    return new Subscription(this, observer);
                }
            }

            observer.OnCompleted();
            return new EmptyDisposable();
        }

        private void Unsubscribe(IObserver<EventEntry> observer)
        {
            lock (this.lockObject)
            {
                this.observers = this.observers.Where(x => !observer.Equals(x)).ToList().AsReadOnly();
            }
        }

        public void OnCompleted()
        {
            var currentObservers = this.TakeObserversAndFreeze();

            if (currentObservers != null)
            {
                Parallel.ForEach(currentObservers, observer => observer.OnCompleted());
            }
        }

        public void OnError(Exception error)
        {
            var currentObservers = TakeObserversAndFreeze();

            if (currentObservers != null)
            {
                Parallel.ForEach(currentObservers, observer => observer.OnError(error));
            }
        }

        public void OnNext(EventEntry value)
        {
            foreach (var observer in this.observers)
            {
                // TODO: should I isolate errors (i.e: try/catch around each OnNext call)?
                observer.OnNext(value);
            }
        }

        private ReadOnlyCollection<IObserver<EventEntry>> TakeObserversAndFreeze()
        {
            lock (this.lockObject)
            {
                if (!this.isFrozen)
                {
                    this.isFrozen = true;
                    var copy = this.observers;
                    this.observers = new List<IObserver<EventEntry>>().AsReadOnly();

                    return copy;
                }

                return null;
            }
        }

        private sealed class Subscription : IDisposable
        {
            private IObserver<EventEntry> observer;
            private EventEntrySubject subject;

            public Subscription(EventEntrySubject subject, IObserver<EventEntry> observer)
            {
                this.subject = subject;
                this.observer = observer;
            }

            public void Dispose()
            {
                var current = Interlocked.Exchange<IObserver<EventEntry>>(ref this.observer, null);
                if (current != null)
                {
                    this.subject.Unsubscribe(current);
                    this.subject = null;
                }
            }
        }

        private sealed class EmptyDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
