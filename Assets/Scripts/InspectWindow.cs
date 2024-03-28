using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectWindow : MonoBehaviour
{
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] TextMeshProUGUI _itemDescription;

    public void InspetObject(ItemDetails itemDetails)
    {
        //activate gameobject
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        SetupWindow(itemDetails);
    }
    public void CloseInspect()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
    public void SetupWindow(ItemDetails itemDetails)
    {
        _itemImage.sprite = itemDetails.Icon;
        _itemName.text = itemDetails.ItemName;
        _itemDescription.text = itemDetails.Description;
    }
    public void SetupWindow(Sprite image, string name, string description)
    {
        _itemImage.sprite = image;
        _itemName.text = name;
        _itemDescription.text = description;
    }
    public void SetImage(Sprite image)
    {
        _itemImage.sprite = image;
    }
    public void SetName(string name)
    {
        _itemName.text = name;
    }
    public void SetDescription(string description)
    {
        _itemDescription.text = description;
    }
}
