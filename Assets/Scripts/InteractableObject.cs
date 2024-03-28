using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableObject : MonoBehaviour, IPointerClickHandler, IInteractable
{
    abstract public void Interact();
    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }

}
