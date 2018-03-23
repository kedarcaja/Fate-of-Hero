using UnityEngine;
using UnityEngine.UI;

public class PlayAnim : MonoBehaviour {

    [SerializeField]
	private GameObject transInObject;
    [SerializeField]
    private Animation transOutAnim;
    [SerializeField]
    private Animation transInAnim;

    public void Press()
    {
        transInObject.SetActive(true);
        transInAnim.Play();
        transOutAnim.Play();
    }
}