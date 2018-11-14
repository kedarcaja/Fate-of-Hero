using UnityEngine;
using UnityEngine.Events;

public class DialogInterpret : MonoBehaviour
{
    public Dialog dialog;
    public bool IsEnable;
    public UnityEvent[] OnDialogEnd;
    public UnityEvent[] OnDialogStart;


    private void Awake()
    {

        dialog.Init();// filling the default delegates
        dialog.OnEnd += () =>
        {
            Destroy(gameObject);
            foreach (UnityEvent e in OnDialogEnd)
            {
                e.Invoke();
            }
              
           
        };
        dialog.OnStart += () =>
        {
            foreach (UnityEvent e in OnDialogStart)
            {
                e.Invoke();
            }
        };
        dialog.WasPlayed = false;
    }
    private void Update()
    {
        if (dialog.IsPlaying && Input.GetKeyDown(KeyCode.K))
        {
            dialog.OnEnd();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsEnable&&!dialog.WasPlayed)
        {
            dialog.OnStart();

        }
    }

}

