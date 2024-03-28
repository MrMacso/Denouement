using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue/New ItemDetails Container")]
public class ItemDetails : ScriptableObject
{
    public Sprite Icon;

    public string ItemName;

    [TextArea(5, 10)]
    public string Description;
}
