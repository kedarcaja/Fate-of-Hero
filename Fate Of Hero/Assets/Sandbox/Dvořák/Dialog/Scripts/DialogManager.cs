using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{

    private static DialogManager instance;
    public static DialogManager Instance { get { return instance; } }
    public AudioSource AudioPlayer { get { return GetComponent<AudioSource>(); } }
    [SerializeField]
    private Text subtitleArea;
    public Text SubtitleArea { get { return subtitleArea; } }
    [SerializeField]
    private List<GameObject> decisions;
    public List<GameObject> Decisions { get { return decisions; } }
    [SerializeField]
    private GameObject decisionBG;
    private void Awake()
    {
        instance = FindObjectOfType<DialogManager>();
    }

    public void HideShowDecisions(int count, bool b)
    {
        for (int i = 0; i < count; i++)
        {
            decisions[i].SetActive(b);
        }
        decisionBG.SetActive(b);

    }
    public void SetDecision(int index, string text)
    {
        decisions[index].GetComponent<Text>().text = text;
    }
    public void Highlite(Text tx)
    {
        UnHighlite();
        tx.color = Color.yellow;
    }
    public void UnHighlite()
    {
        foreach (GameObject g in decisions)
        {

            g.GetComponent<Text>().color = Color.white;
        }
    }
    public void OptionNavigate(int index)
    {

        Highlite(decisions[index].GetComponent<Text>());

    }
    public void MouseHighlite(GameObject over)
    {
        UnHighlite();
        Highlite(over.GetComponent<Text>());
    }
}
