using UnityEngine;
using UnityEngine.Events;

public class DialogInterpret : MonoBehaviour
{

    public SceneManager sceneManager;
    public Dialog dialog;
    public bool IsEnable;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnDialogStart;
    public UnityEvent OnHelpShow;
    public UnityEvent OnHelpHide;
    private  int kCliickCounter;

    private string descriptionText = "přeskočit dialog";
    private KeyCode key = KeyCode.Space;

    private void Awake()
    {
        dialog.WasPlayed = false;


        dialog.Init();// filling the default delegates
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
            kCliickCounter++;
            sceneManager.DescriptionText.text = descriptionText;
            sceneManager.VisialTextObject.text = key.ToString();
            OnHelpShow.Invoke();
            if(Input.GetKeyDown(KeyCode.Space)&&kCliickCounter==2)
            {
                descriptionText = "opravdu přeskočit dialog";
                OnHelpHide.Invoke();

                kCliickCounter = 0;
                dialog.OnEnd();
            }
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

