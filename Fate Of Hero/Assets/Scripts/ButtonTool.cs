using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonTool : MonoBehaviour {
    #region Variables
    [Header("Button")]
    [SerializeField]
    private Text buttonText;
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color Enter;
    private bool saveExist;
   
 

    #endregion
    #region Unity Metod
    void Start () {
		
		
		
        if (!buttonText) { Debug.LogError("<color=Red><b>ERROR: </b> Text buttonText was not found </color>"); }
    }
	


    public void MouseEnter()
    {
        if (!saveExist) { buttonText.color = Enter; Enter.a = 1; }
    }
    public void MouseLeave()
    {
        if (!saveExist) { buttonText.color = startColor;  startColor.a = 1; }
    }
    #endregion


   public void Back()
    {



        transform.parent.parent.gameObject.SetActive(false);
    }
}
