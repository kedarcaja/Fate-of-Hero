using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public CanvasGroup[] canvasGroup;
    

    public void Click(int index)
    {
        for (int i = 0; i < canvasGroup.Length; i++)
        {
            canvasGroup[i].alpha = 0;
        }
        canvasGroup[index].alpha = 1;
       
    }

    
}
