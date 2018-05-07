using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAction : MonoBehaviour {

	public void SetNextAction(GameObject ToActive)
    {
        ToActive.SetActive(true);
        gameObject.SetActive(false);
    }
}
