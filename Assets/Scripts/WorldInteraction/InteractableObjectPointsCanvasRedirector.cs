using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InteractableObjectPointsCanvasRedirector : MonoBehaviour, IInputListener
{
    //Link Interactable Object
    public InteractableObjectController Listener;

    public float exitDelay = 1.5f;
    float exitTimer;

    public void OnPointerClick()
    {
        exitTimer = -1;
    }

    public void OnPointerEnter()
    {
        if(Listener != null)
        {
            Listener.OnGazedInteractableCanvas();
        }
        exitTimer = -1;
    }

    public void OnPointerExit()
    {
        if (exitDelay == 0)
        {
            if (Listener != null)
            {
                Listener.OnLeftInteractableCanvasGazeWithDelay();
            }
        }
        exitTimer = exitDelay;
    }

    private void OnEnable()
    {
        Vector2 _size = GetComponent<RectTransform>().rect.size;
        GetComponent<BoxCollider>().size = new Vector3(_size.x, _size.y, 0);
        GetComponent<BoxCollider>().center = new Vector3(_size.x * -(GetComponent<RectTransform>().pivot.x - 0.5f), _size.y * -(GetComponent<RectTransform>().pivot.y - 0.5f), 0);

    }

    private void Update()
    {
        if (exitTimer > 0)
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer <= 0)
            {
                if (Listener != null)
                {
                    Listener.OnLeftInteractableCanvasGazeWithDelay();
                }
            }
        }
    }
}
