using UnityEngine;
using UnityEngine.Events;

public class OpenDoor : MonoBehaviour
{
    public enum Mode { unlocked, locked }

    private float Range = 3;
    Animator anim;
    bool IsOpen;
    bool trigger;
    public Mode mode;
    Transform player;
    [SerializeField]
    private string descriptionText;
    public KeyCode key;

    public UnityEvent OnHelpShow;
    public UnityEvent OnHelpHide;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }
    private void Start()
    {

    }


    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < Range)
        {
            trigger = true;

            if (trigger && SceneManager.Instance.DescriptionText.text != descriptionText)
            {

                SceneManager.Instance.DescriptionText.text = descriptionText;
                SceneManager.Instance.VisialTextObject.text = key.ToString();
                OnHelpShow.Invoke();

            }

        }
        else if (trigger && Vector3.Distance(transform.position, player.position) > Range)
        {

            OnHelpHide.Invoke();
            trigger = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && trigger && mode == Mode.unlocked)
        {
            Open();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetTrigger("Open");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            anim.SetTrigger("Close");
        }
    }

    void Helps()
    {


    }
    public void Open()
    {
        switch (mode)
        {
            case Mode.unlocked:
                if (!IsOpen)
                {
                    anim.SetTrigger("Open");
                    IsOpen = true;
                }
                else
                {
                    anim.SetTrigger("Close");
                    IsOpen = false;
                }
                break;
            case Mode.locked:
                if (!IsOpen)
                {
                    anim.SetTrigger("Open");
                    IsOpen = true;
                    mode = Mode.unlocked;
                }
                else
                {
                    anim.SetTrigger("Close");
                    IsOpen = false;
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 1f);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + 0, transform.position.y - 1, transform.position.z + 0), Range);
    }
    public void Unlock()
    {
        mode = Mode.unlocked;
    }
}