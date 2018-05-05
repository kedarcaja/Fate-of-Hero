using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchvementButton : MonoBehaviour {
    #region Variables
    public GameObject achvementList;

    public Sprite neutral, highlight;

    private Image sprite;
    #endregion
    #region Metods
    private void Awake()
    {
        sprite = GetComponent<Image>();
    }
    public void Click()
    {
        if (sprite.sprite == neutral)
        {
            sprite.sprite = highlight;
            achvementList.SetActive(true);
        }
        else
        {
            sprite.sprite = neutral;
            achvementList.SetActive(false);
        }
    }

   
     #endregion
}
