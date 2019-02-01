using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CutSceneInterpret : MonoBehaviour
{
    public Dialog dialog;
    public UnityEvent OnDialogEnd;
    public UnityEvent OnDialogStart;
   

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

    private void Start()
    {
        dialog.OnStart();
    }


    public void next(string scene)
    {
       Application.LoadLevelAsync(scene);
        
    }
}
