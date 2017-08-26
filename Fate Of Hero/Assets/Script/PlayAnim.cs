using UnityEngine;
using UnityEngine.UI;

public class PlayAnim : MonoBehaviour {
  
	public GameObject transInObject;
	public Animation transOutAnim;
	public Animation transInAnim;
   

 
    public void Press()
    {
        transInObject.SetActive(true);
        transInAnim.Play();
        transOutAnim.Play();
       
    }
}