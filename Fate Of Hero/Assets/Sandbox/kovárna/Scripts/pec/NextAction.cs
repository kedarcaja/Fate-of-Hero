using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAction : MonoBehaviour {

	public void SetNextAction(GameObject ToActive)
    {
        ToActive.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ChooseAction(GameObject Active)
    {
        for(int i = 0; Active.transform.parent.childCount>i;i++)
        {

            if(Active.transform.parent.GetChild(i)!= Active)
            Active.transform.parent.GetChild(i).gameObject.SetActive(false);

        }
        Active.SetActive(true);

    }
}
