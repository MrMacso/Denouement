using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CluesMenuAnims : MonoBehaviour
{
    public GameObject MenuBox;

    private Sequence MenuOpeningSeq;
    bool reloading = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpMenu()
    {
            MenuOpeningSeq = DOTween.Sequence();
            MenuOpeningSeq.Pause();

            MenuOpeningSeq.SetAutoKill(false);

            Debug.Log("Creating anims");
            MenuOpeningSeq.Append(MenuBox.GetComponent<Image>().DOFade(1, 0.25f))
                .Join(MenuBox.GetComponent<RectTransform>().DOAnchorPosX(0, 0.25f));
        reloading = true;
    }

    public void StartAnims()
    {
        if (!reloading)
        {
            SetUpMenu();
        }

        MenuBox.SetActive(true);
        MenuOpeningSeq.Play();

    }

    public void ReverseAnims()
    {
        MenuOpeningSeq.Rewind();
        //MenuOpeningSeq.OnComplete(MenuBox.SetActive(false));
    }

}
