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

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.alpha == 0)
            {
                map.alpha = 1;
                map.blocksRaycasts = true;
                MouseManager.Instance.EnableCursor();
            }
            else
            {
                map.alpha = 0;
                map.blocksRaycasts = false;
                MouseManager.Instance.EnableCursor();
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
