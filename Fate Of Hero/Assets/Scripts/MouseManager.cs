using UnityEngine;
using UnityEngine.Events;


public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer; // layermask used to isolate raycasts against clickable layers
   
    private float Range;
    public Texture2D pointer; // normal mouse pointer
    public Texture2D target; // target mouse pointer
    public Texture2D doorway; // doorway mouse pointer

    public EventVector3 OnClickEnviroment;
    private void Awake()
    {
        //Range = heroController.MoveRange;
    }
    void Update()
    {
        
        // Raycast into scene
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 45, clickableLayer.value)&&PlayerScript.Instance.CanMove)
        {
           
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);                
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }
            if (Input.GetMouseButton(0))
            {              
                    OnClickEnviroment.Invoke(hit.point);              
            }

        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }