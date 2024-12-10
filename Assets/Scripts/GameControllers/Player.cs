using System;
using System.Collections;
using System.Collections.Generic;
using Lessons.Architecture.GameSystem;
using UnityEngine;

public interface IPlayerPosition
{
    public Vector3 Position { get; }
}

public class Player : MonoBehaviourObserver<GameInput> , IPlayerPosition, IObservable<IPlayerPosition>
{
    [SerializeField]
    private float speed = 2.5f;

    private List<IObserver<IPlayerPosition>> _playerPositionObservers = new();

    public Vector3 Position => transform.position;

    public override void OnNext(GameInput value)
    {
        this.transform.position += value.Input * this.speed;
        NotifyObserverrs();
    }

    private void NotifyObserverrs()
    {
        foreach (var observer in _playerPositionObservers)
        {
            observer.OnNext(this);
        }
    }

    public IDisposable Subscribe(IObserver<IPlayerPosition> observer)
    {
        _playerPositionObservers.Add(observer);
        observer.OnNext(this);
        return new Unsubscriber<IPlayerPosition>(_playerPositionObservers, observer);
    }
}
