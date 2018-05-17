using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Piece : MonoBehaviour {
    #region Variables
    private float speed = 0.035f;
    [SerializeField]
    private List <Image> imgs;
    private  int index;
  

    
    private int currentTimerTime;
    private GameObject activePiece;
    public  GameObject ActivePiece{

        get
        {
            return activePiece;
        }
      private set
        {
            value = activePiece;
            
        }

}
    #endregion

    private void Start()
    {
        StartCoroutine(StartTimer());
        imgs = new List<Image>();

      


        for (int i = 0; i < transform.childCount; i++)
        {
            imgs.Add(transform.GetChild(i).GetComponent<Image>());
        }
        activePiece = imgs[index].gameObject;

    }

    private void Update()
    {


        if(!IsEmpty())
            Fade(imgs[index]);
        if (IsEmpty())
        {
            transform.parent.gameObject.SetActive(false);
        }
    }




    private void Fade(Image img)
    {

        activePiece = img.gameObject;

        Color currentColor = img.color;

        currentColor.a -= speed;
        img.color = currentColor;
        if (currentColor.a <= 0 || currentColor.a >= 1)
            speed *= -1;


    }
  


    IEnumerator StartTimer()
    {

        while (!IsEmpty())

        {
          
            yield return new WaitForSecondsRealtime(2);
            imgs[index].color = new Color(1,1,1,1);
            index = Random.Range(0,imgs.Count);    
        }
    }
public void RemoveImage()
    {

        if (!IsEmpty())
        {
            Destroy(imgs[index]);
            imgs.RemoveAt(index);
            index = Random.Range(0, imgs.Count);
        }


    }

   public bool IsEmpty()
    {



        return imgs.Count == 0;
    }
}

