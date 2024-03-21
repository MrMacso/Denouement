using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CluesMenuController : MonoBehaviour
{
    public CluesMenuAnims animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCluesMenu()
    {
        animator.StartAnims();
    }

    public void CloseCluesMenu()
    {
        animator.ReverseAnims();
    }

}
