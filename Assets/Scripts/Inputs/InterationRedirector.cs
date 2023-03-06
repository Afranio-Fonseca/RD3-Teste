using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InterationRedirector : MonoBehaviour, IInputListener
{
    public GameObject Listener;
    public float enterDelay = 0.1f;
    public float exitDelay = 0.1f;
    float enterTimer;
    float exitTimer;

    [SerializeField] bool resizeColliderToRectTransform;
    private void OnEnable()
    {
        if (resizeColliderToRectTransform)
        {
            Vector2 _size = GetComponent<RectTransform>().rect.size;
            GetComponent<BoxCollider>().size = new Vector3(_size.x, _size.y, 0);
            GetComponent<BoxCollider>().center = new Vector3(_size.x * -(GetComponent<RectTransform>().pivot.x - 0.5f), _size.y * -(GetComponent<RectTransform>().pivot.y - 0.5f), 0);
        }
    }

    private void Update()
    {
        if (enterTimer > 0)
        {
            if (Listener.transform.parent != null && transform.parent.parent != null) 
            {
                Listener.transform.parent.parent.gameObject.SetActive(true);
            }

            enterTimer -= Time.deltaTime;

            if (enterTimer <= 0)
            {
                if (Listener != null)
                {
                    Listener.GetComponent<IInputListener>().OnPointerEnter();
                }
            }
        }

        if (exitTimer > 0)
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer <= 0)
            {
                if (Listener != null)
                {
                    Listener.GetComponent<IInputListener>().OnPointerExit();
                }
            }
        }
    }

    public void OnPointerEnter()
    {
        exitTimer = -1;

        if (enterDelay <= 0)
        {
            if (Listener != null)
            {
                if (Listener.transform.parent != null && transform.parent.parent != null) Listener.transform.parent.parent.gameObject.SetActive(true);
                Listener.GetComponent<IInputListener>().OnPointerEnter();
            }
        }
        enterTimer = enterDelay;
    }

    public void OnPointerExit()
    {
        enterTimer = -1;

        if (exitDelay <= 0)
        {
            if (Listener != null)
            {
                if (Listener.transform.parent != null && transform.parent.parent != null) Listener.transform.parent.parent.gameObject.SetActive(false);
                Listener.GetComponent<IInputListener>().OnPointerExit();
            }
        }
        exitTimer = exitDelay;
    }

    public void OnPointerClick()
    {

    }
}
