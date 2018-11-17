using UnityEngine;
using UnityEngine.Events;

public class DialogInterpret : MonoBehaviour
{


    public Dialog dialog;
    public bool IsEnable;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnDialogStart;
   private  int kCliickCounter;

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
            DialogManager.Instance.SkipAtention.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space)&&kCliickCounter==2)
            {
                DialogManager.Instance.SkipAtention.gameObject.SetActive(false);

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

