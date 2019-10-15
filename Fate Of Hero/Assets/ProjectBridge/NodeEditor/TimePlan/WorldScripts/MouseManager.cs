using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseManager : MonoBehaviour
{
	
    [SerializeField]
	private Texture2D pointerUI;   
	public static MouseManager Instance { get; private set; }
  
	private void Awake()
	{
		Instance = FindObjectOfType<MouseManager>();
        Cursor.SetCursor(pointerUI, new Vector2(16, 16), CursorMode.Auto);
    }

    public void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

