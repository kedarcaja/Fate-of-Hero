using UnityEngine;
using UnityEngine.Events;

public class DialogInterpret : MonoBehaviour
{


    public Dialog dialog;
    public bool IsEnable;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnDialogStart;
    private int kCliickCounter;

    private string descriptionText = "přeskočit dialog";
    private KeyCode key = KeyCode.Space;

    private void Awake()
    {
        dialog.WasPlayed = false;


        dialog.Init(); // filling the default delegates
        dialog.OnEnd += () =>
        {
            Destroy(gameObject);

            OnDialogEnd.Invoke();



        };
        dialog.OnStart += () =>
        {

            OnDialogStart.Invoke();

        };


    }

    private void Update()
    {

        if (dialog.IsPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.Instance.DescriptionText.text = descriptionText;
            SceneManager.Instance.VisialTextObject.text = key.ToString();
            SceneManager.Instance.ShowHelp();
            kCliickCounter++;

            if (kCliickCounter == 2)
            {
                SceneManager.Instance.HideHelp();
                kCliickCounter = 0;
                dialog.OnEnd();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsEnable && !dialog.WasPlayed)
        {
            dialog.OnStart();

        }
    }

}

