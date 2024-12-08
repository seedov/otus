using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Lessons.Architecture.GameSystem
{
    public class GameInstaller : MonoBehaviour
    {
        private List<IDisposable> _disposables = new();
        private GameStateFsm _gameState = new();

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

            //подписываем gameState на gameStateSwitcher
            //т.е. gameState теперь будет получать уведомления от gameStateSwitcher
            _disposables.Add(_gameStateSwitcher.Subscribe(_gameState));

            _disposables.Add(_gameState.Subscribe(_userInput));
            _disposables.Add(_gameState.Subscribe(_gameStateLogger));

            _disposables.Add(_userInput.Subscribe(_player));


            _disposables.Add(_player.Subscribe(_cameraFollower));

        }



        private void OnDestroy()
        {
            foreach (var disposable in _disposables) { disposable.Dispose(); }
            _disposables.Clear();
        }
    }
}