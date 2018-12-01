using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour {

public IEnumerator Hide()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }
    private void OnEnable()
    {
        StartCoroutine(Hide());
    }
}
