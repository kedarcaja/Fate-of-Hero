using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAction : MonoBehaviour {

	public void SetNextAction(GameObject ToActive)
    {
        ToActive.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ShowHelp(GameObject Help)
    {
        Help.SetActive(true);
    }
    public void HideHelp(GameObject Help)
    {
        Help.SetActive(false);
    }
}
