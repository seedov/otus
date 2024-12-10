using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Architecture.GameSystem
{
    public class GameInstaller : MonoBehaviour
    {
        private GameStateFsm _gameState;

        [SerializeField]
        private GameStateSwithcer _gameStateSwitcher;

        [SerializeField]
        private KeyboardInput _userInput;

        [SerializeField]
        private CameraFollower _cameraFollower;

        [SerializeField]
        private GameStateLogger _gameStateLogger;

        [SerializeField]
        private Player _player;

        private void Awake()
        {
            _gameState = new GameStateFsm(new[]{ _gameStateSwitcher });

            _userInput.Initialize(new[] { _gameState });

            _gameStateLogger.Initialize(new[] { _gameState });

            _player.Initialize(new[] { _userInput });

            _cameraFollower.Initialize(new[] { _player });

        }

    }
}