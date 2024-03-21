using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteligentWaypoint : MonoBehaviour
{
    [SerializeField] GameObject CollectableItem = null;
    [SerializeField] string PuppetName = string.Empty;
    [SerializeField] float Speed = 0f;
    [SerializeField] bool IsCollectItem = false;
    [SerializeField] bool IsDropItem = false;


    //[SerializeField] bool IsAnimate = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CollisionHappened");
        if (other.gameObject.name == PuppetName)
        {
            var followPathAI = other.gameObject.GetComponent<FollowPathAI>();
            if (Speed != 0f)
            {
                followPathAI.SetMoveSpeed(Speed);
            }
            if (IsCollectItem)
            {
                followPathAI.AttachObject(CollectableItem);
            }
            if (IsDropItem)
            {
                followPathAI.DropObject();
            }
        }
    }



}
