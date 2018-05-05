using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour {
    public string achievmentName;
	
	void Start () {
		
	}
	

	void Update () {
		
	}
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject(-1))
        {
            AchivementManager.Instance.EarnAchievment(achievmentName);
        }
       
    }
}
