using System;
using System.Collections.Generic;

namespace Lessons.Architecture.GameSystem
{
    public enum GameState
    {
        MainMenu,
        Play,
        Pause
    }

    public enum GameEvent
    {
        Start,
        Pause,
        Resume
    }


    public class GameStateFsm : 
        IDisposable,
        IObserver<GameEvent>, //Мы являмся слушателями игровых событий
        IObservable<GameState>//Мы являемся еще и продюсерами именения игрового состояния. Т.е. на нас можно подписаться
    {
        private List<IObserver<GameState>> _gameStateObservers = new();

        private GameState _gameState;
        public GameState CurrentState => _gameState;

        private List<IDisposable> _subscriptions = new();

        public GameStateFsm(IObservable<GameEvent>[] gameStateSources)
        {
            foreach (var source in gameStateSources)
            {
                _subscriptions.Add(source.Subscribe(this));
            }
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(GameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameEvent.Start:
                    SetState(GameState.Play); 
                    break;
                case GameEvent.Pause:
                    SetState ( GameState.Pause);
                    break;
                case GameEvent.Resume:
                    SetState(GameState.Play);
                    break;
            }
        }

        private void SetState(GameState state)
        {
            if (_gameState == state)
                return;

            _gameState = state;
            foreach (var observer in _gameStateObservers)
            {
                observer.OnNext(state);
            }
        }

        public IDisposable Subscribe(IObserver<GameState> observer)
        {
            _gameStateObservers.Add(observer);
            observer.OnNext(_gameState);
            return new Unsubscriber<GameState>(_gameStateObservers, observer);
        }

        public void Dispose()
        {
            foreach(var subsciption in _subscriptions)
            {
                subsciption.Dispose();
            }
            _subscriptions.Clear();
        }
    }
}