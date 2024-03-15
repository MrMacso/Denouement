using UnityEngine;
using UnityEngine.AI;

internal class RotateToSelectedDestination : IState
{
    private readonly FollowPathAI _puppet;
    static readonly float ROTATION_SPEED = 5.0f;

    Quaternion _lookRotation;
    Vector3 _direction;

    public RotateToSelectedDestination(FollowPathAI puppet)
    {
        _puppet = puppet;
    }

    public void Tick()
    {
        //rotate us over time according to speed until we are in the required rotation
        _puppet.transform.rotation = Quaternion.Slerp(_puppet.transform.rotation, _lookRotation, Time.deltaTime * ROTATION_SPEED);
    }

    public void OnEnter()
    {
        //find the vector pointing from our position to the target
        _direction = (_puppet.Destination.transform.position - _puppet.transform.position).normalized;
        _direction.y = 0;
        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);
    }

    public void OnExit() => _puppet.SetIsCheckingAngle(false);

}
