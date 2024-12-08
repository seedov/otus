using System;
using System.Collections.Generic;

namespace Lessons.Architecture.GameSystem
{


        public sealed class Unsubscriber<T> : IDisposable
        {
            private readonly IList<IObserver<T>> _observers;
            private readonly IObserver<T> _observer;

            public Unsubscriber(IList<IObserver<T>> observers, IObserver<T> observer)
            {
                _observer = observer;
                _observers = observers;
            }

            public void Dispose() => _observers.Remove(_observer);
        }
    
}