using System;
using System.Collections.Generic;
using Lessons.Architecture.GameSystem;
using UnityEngine;

public struct GameInput
{
    public readonly Vector3 Input;
    public GameInput(Vector3 input)
    {
        Input = input;
    }
}

public class KeyboardInput : MonoBehaviourObserver<GameState>, IObservable<GameInput>
{
    private List<IObserver<GameInput>> _userInputObservers = new();

    private bool _canProcessUserInput = false;


    public IDisposable Subscribe(IObserver<GameInput> observer)
    {
        _userInputObservers.Add(observer);
        return new Unsubscriber<GameInput>(_userInputObservers, observer);
    }

    private void Update()
    {
        if(!_canProcessUserInput)
            return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.Move(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.Move(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.Move(Vector3.right);
        }

    }

    private void Move(Vector3 direction)
    {
        foreach (var observer in _userInputObservers)
        {
            observer.OnNext(new GameInput(direction));
        }

    }

    public override void OnNext(GameState value)
    {
        _canProcessUserInput = value == GameState.Play;
    }
}
