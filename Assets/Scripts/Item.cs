using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : InteractableObject
{
    [SerializeField] ItemDetails itemDetails;
    [SerializeField] InspectWindow inspectWindow;

    public override void Interact()
    {
        inspectWindow.InspetObject(itemDetails);
    }
}
