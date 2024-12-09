
using UnityEngine;
using VContainer.Unity;
using VContainer;
using System.Collections.Generic;
using System;

namespace Lessons.Architecture.GameSystem
{


    public class VContainerGameInstaller : LifetimeScope
    {
        private List<IDisposable> _disposables = new();

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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameStateFsm>(Lifetime.Singleton);
            builder.RegisterComponent(_gameStateSwitcher);
            builder.RegisterComponent(_userInput);
            builder.RegisterComponent(_cameraFollower);
            builder.RegisterComponent(_gameStateLogger);
            builder.RegisterComponent(_player);

        }

        private void Start()
        {
            var _gameState = Container.Resolve<GameStateFsm>();
            var _gameStateSwitcher = Container.Resolve<GameStateSwithcer>();
            var _userInput = Container.Resolve<KeyboardInput>(); 
            var _cameraFollower = Container.Resolve<CameraFollower>();
            var _gameStateLogger = Container.Resolve<GameStateLogger>();
            var _player = Container.Resolve<Player>();

            //подписываем gameState на gameStateSwitcher
            //т.е. gameState теперь будет получать уведомления от gameStateSwitcher
            _disposables.Add(_gameStateSwitcher.Subscribe(_gameState));

            _disposables.Add(_gameState.Subscribe(_userInput));
            _disposables.Add(_gameState.Subscribe(_gameStateLogger));

            _disposables.Add(_userInput.Subscribe(_player));


            _disposables.Add(_player.Subscribe(_cameraFollower));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var disposable in _disposables) { disposable.Dispose(); }
            _disposables.Clear();
        }


    }
}