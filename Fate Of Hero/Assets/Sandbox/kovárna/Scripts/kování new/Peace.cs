using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Peace : MonoBehaviour {
    private float speed = 0.035f;
    [SerializeField]
   private Image[] imgs;
    private int TimerTime = 999;
    private int currentTimerTime;


    private int index;
    private void Start()
    {
        StartCoroutine(StartTimer());
  


        imgs = new Image[14];

        for (int i = 0; i < transform.childCount; i++)
        {
            imgs[i] = transform.GetChild(i).GetComponent<Image>();
        }
      

    }

    private void Update()
    {

      
        Fade(imgs[index]);

       
    }




    private void Fade(Image img)
    {



        Color currentColor = img.color;

        currentColor.a -= speed;
        img.color = currentColor;
        if (currentColor.a <= 0 || currentColor.a >= 1)
            speed *= -1;


    }
  


    IEnumerator StartTimer()
    {

        while (true)
        {
          
            yield return new WaitForSecondsRealtime(2);
            imgs[index].color = new Color(1,1,1,1);
            index = Random.Range(0,imgs.Length);    
        }
    }
}

