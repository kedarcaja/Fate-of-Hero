using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseManager : MonoBehaviour
{
	public LayerMask MoveLayer, OpenLayer, DoorLayer, ForbiddenLayer,AttackLayer;
    [SerializeField]
    private Transform player;
	public Texture2D pointerUI;
	public Texture2D pointerMove;
    public Texture2D pointerAttack;
    public Texture2D pointerOpen;
	public Texture2D pointerDoor;
	public Texture2D pointerForbidden;
    private Texture2D currentCursor;
    public bool CanClick;

    public float MoveClickRange
    {
        get
        {
            return moveClickRange;
        }
    }

    
	public static MouseManager Instance;
    public GameObject PointPref;
    private GameObject PointInstance;
    [SerializeField]
    private float moveClickRange;
	private void Awake()
	{
		Instance = FindObjectOfType<MouseManager>();
	}

	void Update()
	{

		if (EventSystem.current.IsPointerOverGameObject())
		{
			SetCursor(pointerUI);
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
            
            if (Math.Pow(2, hit.collider.gameObject.layer) == MoveLayer.value)
			{
				CanClick = true;
				SetCursor(pointerMove);
			}
			else if (Math.Pow(2, hit.collider.gameObject.layer) == DoorLayer)
			{
				CanClick = true;
				SetCursor(pointerDoor);
			}
			else if (Math.Pow(2, hit.collider.gameObject.layer) == OpenLayer.value)
			{
				CanClick = true;
				SetCursor(pointerOpen);
			}
            else if (Math.Pow(2, hit.collider.gameObject.layer) == AttackLayer.value)
            {
                CanClick = true;
                SetCursor(pointerAttack);
            }
            //else if ((Math.Pow(2, hit.collider.gameObject.layer) != MoveLayer.value && Math.Pow(2, hit.collider.gameObject.layer) != DoorLayer && Math.Pow(2, hit.collider.gameObject.layer) != OpenLayer.value)|| Vector3.Distance(PlayerScript.Instance.transform.position, hit.point) > MoveClickRange)
            //{
            //    CanClick = false;
            //    SetCursor(pointerForbidden);
            //}

    //        if (Input.GetMouseButton(0) && CanClick && Vector3.Distance(PlayerScript.Instance.transform.position, hit.point) <= MoveClickRange)
    //        {
    //            if (PointInstance != null)
    //            {
    //                Destroy(PointInstance);
    //            }
    //            PointInstance = Instantiate(PointPref, hit.point, Quaternion.identity);
				//if (PlayerScript.Instance.Stats.TargetVector.Target != null)
				//{
				//	PlayerScript.Instance.Stats.TargetVector.Target = null;
				//}
    //            PlayerScript.Instance.SetDestination(hit.point);
    //        }
          

        }

        
	}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.position, MoveClickRange);
    }
    public void SetCursor(Texture2D texture)
	{
        if (texture == currentCursor)
        {
            return;
        }
        currentCursor = texture;
		Cursor.SetCursor(texture, new Vector2(16, 16), CursorMode.Auto);
        
	}
	
}

[Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
