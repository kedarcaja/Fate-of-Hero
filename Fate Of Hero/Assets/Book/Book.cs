using System.Collections;
using System.Collections.Generic;
using UI.Components;
using UnityEngine;
using Unity.Extensions;
using UI.Components;

public class Book : MonoBehaviour
{
    [SerializeField]
    private CanvasGroupSwitchsGroup bookMarks;

    bool isOpen = false;

    private CanvasGroup group;

    public static Book Instance { get; private set; }

    private void Awake()
    {
        Instance = FindObjectOfType<Book>();
        group = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        Debug.Log(isOpen);
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
    }
    public bool IsActive()
    {
        return isOpen;
    }
    public void Open()
    {
      if (!group.IsActive(true))
        {
            group.Active(true);
            MouseManager.Instance.EnableCursor();
            isOpen = true;
        }
      
    }

    public void Close()
    {
        if (group.IsActive(true))
        {
            group.Deactive(true);
            MouseManager.Instance.DisableCursor();
            isOpen = false;
        }
    }
}
