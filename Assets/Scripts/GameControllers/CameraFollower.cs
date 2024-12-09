using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour, IObserver<IPlayerPosition>
{
    [SerializeField]
    private Vector3 _offset;
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(IPlayerPosition playerPosition)
    {
        transform.position = playerPosition.Position + _offset;
    }

}
