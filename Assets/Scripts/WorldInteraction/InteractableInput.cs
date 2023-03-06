using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(InteractableView))]

public class InteractableInput : MonoBehaviour, IInputListener
{
    //public Image overlay;
    InteractableView interactableView;
    [SerializeField] float totalTimeToInteract = 1f;
    float timer = 0;
    bool isTouched = false;

    RectTransform rectTransform;
    BoxCollider boxCollider;

    [Tooltip("Activate to redirect input to CanvasBlocker, so it keeps turned on")]
    [SerializeField] bool redirectInputToPointsCanvas;
    [SerializeField] public GameObject Listener;
   
    public float enterDelay = 0;
    public float exitDelay = 0;
    float enterTimer;
    float exitTimer;

    private void Awake()
    {
        interactableView = GetComponent<InteractableView>();
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateColliderCenterAndSize(rectTransform.rect.size));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Update()
    {
        //
        // START REDIRECTION LOGIC
        //

        if (redirectInputToPointsCanvas)
        {
            if (enterTimer > 0)
            {
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
        //
        // END REDIRECTION LOGIC
        //

        if (isTouched)
        {
            timer += Time.deltaTime;
            interactableView.UpdateLoadingFill(timer / totalTimeToInteract);
            //GameManager.Instance.Fire_OnInteractionTimerValueChanged(timer / totalTimeToInteract);

            //overlay.fillAmount = (timer / 3f);
            if (timer >= totalTimeToInteract)
            {
                OnPointerClick();
                timer = 0;
                isTouched = false;
            }
        }
        else
        {
            //overlay.fillAmount = 0;
        }

        
    }

    public virtual void OnPointerEnter()
    {
        isTouched = true;
        timer = 0;
        interactableView.ChangeLoadingBarState(true);
        if(this.gameObject.activeInHierarchy)
            StartCoroutine(UpdateColliderCenterAndSize(rectTransform.rect.size));

        // Redirection
        if (redirectInputToPointsCanvas)
        {
            exitTimer = -1;

            if (enterDelay <= 0)
            {
                if (Listener != null)
                {
                    Listener.GetComponent<IInputListener>().OnPointerEnter();
                }
            }
            enterTimer = enterDelay;
        }
    }

    public virtual void OnPointerExit()
    {
        isTouched = false;
        timer = 0;
        interactableView.ChangeLoadingBarState(false);

        if(this.gameObject.activeInHierarchy)
            StartCoroutine(UpdateColliderCenterAndSize(rectTransform.rect.size));

        if (redirectInputToPointsCanvas)
        {
            enterTimer = -1;

            if (exitDelay <= 0)
            {
                if (Listener != null)
                {
                    Listener.GetComponent<IInputListener>().OnPointerExit();
                }
            }
            exitTimer = exitDelay;
        }
    }

    public virtual void OnPointerClick()
    {
        interactableView.ChangeLoadingBarState(false);
    }

    IEnumerator UpdateColliderCenterAndSize(Vector2 previousSize)
    {
        Vector2 _size = rectTransform.rect.size;

        ContentSizeFitter contentSizeFitter = GetComponent<ContentSizeFitter>();

        if (contentSizeFitter)
        {
            while (previousSize == _size)
            {
                _size = rectTransform.rect.size;
                yield return null;
            }
        }
        
        boxCollider.size = new Vector3(_size.x, _size.y, 1);
        boxCollider.center = new Vector3(_size.x * -(rectTransform.pivot.x - 0.5f), _size.y * -(rectTransform.pivot.y - 0.5f), 0);
    }
}
