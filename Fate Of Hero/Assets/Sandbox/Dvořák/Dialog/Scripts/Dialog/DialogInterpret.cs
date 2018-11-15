using UnityEngine;
using UnityEngine.Events;

public class DialogInterpret : MonoBehaviour
{


    public Dialog dialog;
    public bool IsEnable;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnDialogStart;


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
    private void Start()
    {


      


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

