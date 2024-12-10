using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourObserver<T> : MonoBehaviour, IObserver<T>
{
    private List<IDisposable> _subscriptions = new List<IDisposable>();

    public virtual void OnCompleted()
    {
    }

    public virtual void OnError(Exception error)
    {
    }

    public abstract void OnNext(T value);

    public void Initialize(IObservable<T>[] observables)
    {
        foreach (var observable in observables)
        {
            _subscriptions.Add(observable.Subscribe(this));
        }
    }

    protected virtual void OnDestroy()
    {
        foreach(var subcription in _subscriptions)
        {
            subcription.Dispose();
        }
        _subscriptions.Clear();
    }
}
