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
   
    [Space]

    [Header("Zvuk")]
    [SerializeField]
    private AudioClip sound;
    private Button Button { get { return GetComponent<Button>(); } }
    private AudioSource Source { get { return GetComponent<AudioSource>(); } }
    #endregion
    #region Unity Metod
    void Start () {
		gameObject.AddComponent<AudioSource>();
		Source.clip = sound;
		Source.playOnAwake = false;
		Button.onClick.AddListener(() => PlaySound());
        if (!buttonText) { Debug.LogError("<color=Red><b>ERROR: </b> Text buttonText was not found </color>"); }
    }
	
	void PlaySound () {
		Source.PlayOneShot(sound);
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
}
