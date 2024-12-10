using System;
using System.Collections;
using System.Collections.Generic;
using Lessons.Architecture.GameSystem;
using UnityEngine;

public class GameStateLogger : MonoBehaviourObserver<GameState>
{
    public override void OnNext(GameState gameState)
    {
        Debug.Log($"game state swithced to {gameState}");
    }

}
