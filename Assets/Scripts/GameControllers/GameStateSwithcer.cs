using System;
using System.Collections.Generic;
using UnityEngine;
namespace Lessons.Architecture.GameSystem
{
    //генератор событий изменения состояния игры. Его слушают
    public class GameStateSwithcer : MonoBehaviour, IObservable<GameEvent>
    {
        private List<IObserver<GameEvent>> _observers = new ();

        public IDisposable Subscribe(IObserver<GameEvent> observer)
        {
            //подписываем observer на себя
            _observers.Add(observer);
            
            return new Unsubscriber<GameEvent>(_observers, observer);
        }

        [ContextMenu("Start")]
        public void TryStartGame()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(GameEvent.Start);
            }
        }

        [ContextMenu("Pause")]
        public void TryPauseGame()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(GameEvent.Pause);
            }
        }

        [ContextMenu("Resume")]
        public void TryResumeGame()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(GameEvent.Resume);
            }
        }

        private void OnDestroy()
        {
            //сообщаем всем подписчикам что мы завершили работу
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

    }
}