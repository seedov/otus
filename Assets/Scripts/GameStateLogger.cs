using System;
using System.Collections;
using System.Collections.Generic;
using Lessons.Architecture.GameSystem;
using UnityEngine;

public class GameStateLogger : MonoBehaviour, IObserver<GameState>
{
    public void OnCompleted()
    {
        Debug.Log("Completed");
    }

    public void OnError(Exception error)
    {
        Debug.LogError(error.ToString());
    }

    public void OnNext(GameState gameState)
    {
        Debug.Log($"game state swithced to {gameState}");
    }

}
