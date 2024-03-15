using UnityEngine;
using UnityEngine.AI;

internal class MoveToDestination : IState
{
    private readonly FollowPathAI _puppet;

    private static float MOVE_SPEED = 3.0f;
    private static float STAND_SPEED = 0f;
    
    private Vector3 _lastPosition = Vector3.zero;

    public float TimeStuck;

    public MoveToDestination(FollowPathAI puppet)
    {
        _puppet = puppet;
    }
       
    public void Tick()
    {
        if (Vector3.Distance(_puppet.transform.position, _lastPosition) <= 0f)
            TimeStuck += Time.deltaTime;

        _lastPosition = _puppet.transform.position;
    }

    public void OnEnter() 
    {
        TimeStuck = 0f;
        _puppet.SetMoveSpeed(MOVE_SPEED);
    }

    public void OnExit() 
    {
        _puppet.SetMoveSpeed(STAND_SPEED);
    }

}
