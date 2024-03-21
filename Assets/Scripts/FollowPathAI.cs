using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathAI : MonoBehaviour
{
    StateMachine _stateMachine;
    [SerializeField] Transform HandTransform = null;
    public List<Transform> DestinationList { get; set; }
    public Transform Destination { get; set; }

    GameObject HoldObject = null;
    int _destinationIndex = 0;

    float _moveSpeed = 0f; 
    float _lookAngle = 1.0f;

    bool IsActive = false;
    bool IsCheckingAngle = false;

    private static readonly float MARGIN = 0.2f;
    
    private void Awake()
    {
        _stateMachine = new StateMachine();
        //states
        var lookForDestination = new LookForDestination(this);
        var moveToDirection = new MoveToDestination(this);
        var rotateToDirection = new RotateToSelectedDestination(this);
        var endOfPath = new EndOfPath(this);

        //transitions between states
        AT(lookForDestination, rotateToDirection, NeedToRotate());
        AT(rotateToDirection, moveToDirection, HasDestination());
        AT(moveToDirection, lookForDestination, HasDestinationInList());
        AT(lookForDestination, endOfPath, NoDestination());
 
        _stateMachine.SetState(lookForDestination);


        //conditions for the transitions
        Func<bool> NoDestination() => () => Destination == null;
        Func<bool> HasDestinationInList() => () => _destinationIndex <= DestinationList.Count 
                                            && Vector3.Distance(transform.position, Destination.transform.position) < MARGIN;
        Func<bool> NeedToRotate() => () => !IsFacingToDirection() && Destination != null;
        Func<bool> HasDestination() => () => Destination != null && IsFacingToDirection() 
                                             && Vector3.Distance(transform.position, Destination.transform.position) > MARGIN;

        //At: add transition
        void AT(IState to, IState from, Func<bool> condition)
        {
            _stateMachine.AddTransition(to, from, condition);
        }
    }
    void Update()
    {
        if(IsActive) 
        {
            _stateMachine.Tick();

            
            MoveForward();

            if (IsCheckingAngle)
                CheckLookAngle();
            
        }
    }
    public void MoveForward() => transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);

    public void SetMoveSpeed(float speed) => _moveSpeed = speed;
    public void SetActive(bool isActive) => IsActive = isActive;
    public void SetIsCheckingAngle(bool isChecking) => IsCheckingAngle = isChecking;
    public void SetDestinationIndex(int index) => _destinationIndex = index;
    public void ResetState() => _stateMachine.SetState(new LookForDestination(this));


    bool IsFacingToDirection() 
    {
        if (_lookAngle == 0f)
            return true;
        else 
            return false;
    }
    float CheckLookAngle()
    {
        if (Destination != null)
        {
            Vector3 direction = Destination.position - transform.position;
            _lookAngle = Vector3.Angle(transform.forward, direction);

            if (_lookAngle == 0) 
                return _lookAngle =0;
            else
                return _lookAngle;
        }
        return _lookAngle;
    }

    public void SetupDestinationElements(List<Transform> list) 
    {
        DestinationList= list;
        _destinationIndex = -1;
    }

    public void SetDestination() 
    {
        if (_destinationIndex == DestinationList.Count -1)
            Destination = null;
        else
        { 
            _destinationIndex++;  
            Destination = DestinationList[_destinationIndex];
        }
    }

    public void AttachObject(GameObject gameObject)
    {
        if (gameObject == null)
            return;
        else
            HoldObject = gameObject;

        HoldObject.transform.parent = HandTransform.transform;
        HoldObject.transform.position = HandTransform.position;
    }
    public void DropObject()
    {
        if (HoldObject != null)
            HoldObject.transform.parent = null;
        else
            return;
    }
}
