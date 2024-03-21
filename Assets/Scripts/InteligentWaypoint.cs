using UnityEngine;

public class InteligentWaypoint : MonoBehaviour
{
    [Header("Can stay empty")]
    [SerializeField] GameObject CollectableItem = null;
    [SerializeField] string PuppetName = string.Empty;
    [SerializeField] float Speed = 0f;
    [SerializeField] bool IsCollectItem = false;
    [SerializeField] bool IsDropItem = false;


    [SerializeField] bool IsAnimate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == PuppetName)
        {
            var followPathAI = other.gameObject.GetComponent<FollowPathAI>();
            var animator = other.gameObject.GetComponent<Animator>();

            if (followPathAI != null)
            {
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
            else
                Debug.Log("Object doesn't have an FollowPathAI component!");

            if (animator == null)
                Debug.Log("Object doesn't have an Animator component!");

            if (animator!= null && IsAnimate)
            {
                animator.SetBool("IsActive", true);
            }
        }
    }



}
