using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviourObserver<IPlayerPosition>
{
    [SerializeField]
    private Vector3 _offset;


    public override void OnNext(IPlayerPosition playerPosition)
    {
        transform.position = playerPosition.Position + _offset;
    }

}
