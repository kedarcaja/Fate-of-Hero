using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public Inventory CharPanelI;
    public Inventory CharPanelII;

    public void Click()
    {
        if (CharPanelII != null)
        {
            CharPanelII.Open();
        }
    }
}
