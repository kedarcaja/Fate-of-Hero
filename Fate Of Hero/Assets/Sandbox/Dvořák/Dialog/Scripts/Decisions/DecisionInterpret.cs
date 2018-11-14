using System.Collections.Generic;
using UnityEngine;

public class DecisionInterpret : MonoBehaviour
{
    [SerializeField]
    private List<Option> decisions;
    [SerializeField]
    private Dialog dialogBefore;
    private int currentDecisionIndex = 0;
    private void Awake()
    {

        dialogBefore.Init();


    }
    private void Start()
    {
        foreach (Option o in decisions)
        {
            o.DialoAfter.Init();
            o.OnDecision += () =>
            {
                o.DialoAfter.OnStart();
                DialogManager.Instance.HideShowDecisions(decisions.Count, false);
            };
        }
        dialogBefore.OnEnd += () =>
        {
            DialogManager.Instance.HideShowDecisions(decisions.Count, true);
            for (int i = 0; i < decisions.Count; i++)
            {
                DialogManager.Instance.SetDecision(i, decisions[i].Sentence);
            }
            DialogManager.Instance.OptionNavigate(currentDecisionIndex);


        };
    }

    private void Update()
    {
        if (dialogBefore.IsPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            dialogBefore.OnEnd();
            DialogManager.Instance.StopAllCoroutines();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentDecisionIndex > 0)
        {
            currentDecisionIndex--;
            DialogManager.Instance.OptionNavigate(currentDecisionIndex);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentDecisionIndex < decisions.Count - 1)
        {
            currentDecisionIndex++;
            DialogManager.Instance.OptionNavigate(currentDecisionIndex);


        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            decisions[currentDecisionIndex].OnDecision();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            decisions[0].OnDecision();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            decisions[1].OnDecision();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            decisions[2].OnDecision();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            decisions[3].OnDecision();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            decisions[4].OnDecision();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DialogTrigger")
        {
            dialogBefore.OnStart();
        }
    }
    public void MouseSelect(int index)
    {
        decisions[index].OnDecision();
    }
}
