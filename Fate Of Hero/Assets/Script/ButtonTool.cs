using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonTool : MonoBehaviour {
    #region Variables
    [Header("Tlačítka")]
    public Text Buttontext;
    public Color StartColor;
    public Color Enter;
    public bool SaveExist;
   

    [Space]

    [Header("Zvuk")]
    public AudioClip sound;
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    #endregion
    #region Unity Metod
    void Start () {
		gameObject.AddComponent<AudioSource>();
		source.clip = sound;
		source.playOnAwake = false;
		button.onClick.AddListener(() => PlaySound());
	}
	
	void PlaySound () {
		source.PlayOneShot(sound);
	}

    public void MouseEnter()
    {
        if (!SaveExist) { Buttontext.color = Enter; }

    }
    public void MouseLeave()
    {
        if (!SaveExist) { Buttontext.color = StartColor; }

    }
    #endregion
}
