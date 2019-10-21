using System.Collections;
using System.Collections.Generic;
using UI.Components;
using UnityEngine;
using Unity.Extensions;
using UI.Components;
using FourGames;
using UnityEngine.SceneManagement;

public class Book : MonoBehaviour
{
    [SerializeField]
    private CanvasGroupSwitchsGroup bookMarks;

    bool isOpen = false;

    private CanvasGroup group;

    [SerializeField]
    private CanvasGroup map;

    public static Book Instance { get; private set; }

    private void Awake()
    {
        Instance = FindObjectOfType<Book>();
        group = GetComponent<CanvasGroup>();
        
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (group.IsActive(true))
            {
                Close();
            }
            else
            {
                Open();
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.IsActive(true))
            {
                map.Deactive(true);
                MouseManager.Instance.DisableCursor();
                PlayerScript.Instance.EnableMove();
                PlayerScript.Instance.DisableAttack();

            }
            else
            {
                map.Active(true);
                MouseManager.Instance.EnableCursor();
                PlayerScript.Instance.DisableMove();
                PlayerScript.Instance.DisableAttack();


            }

        }
    }
    public bool IsActive()
    {
        return isOpen;
    }
    public void Open()
    {
        if (!IsActive())
        {
            PlayerScript.Instance.CanAttack = false;
            group.Active(true);
            MouseManager.Instance.EnableCursor();
            isOpen = true;
        }

    }

    public void Close()
    {
        if (IsActive())
        {
            PlayerScript.Instance.CanAttack = true;
            group.Deactive(true);
            MouseManager.Instance.DisableCursor();
            isOpen = false;
        }
    }
    
}
