using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForDestination : IState
{
    private readonly FollowPathAI _puppet;

    public LookForDestination(FollowPathAI puppet)
    {
        _puppet = puppet;
    }

    public void Tick() { }

    public void OnEnter()
    {
        _puppet.SetDestination();
        _puppet.SetIsCheckingAngle(true);
    }

    public void OnExit() { }
}
