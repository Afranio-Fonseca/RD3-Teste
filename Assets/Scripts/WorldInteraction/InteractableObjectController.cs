using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectController : MonoBehaviour, IInputListener
{

    public GameObject interactivePointsParent;
    public GameObject myObject;

    [SerializeField]
    Animator animInteraction;

    [SerializeField] bool startOn = false;

    bool isOpen;

    bool isPointerOnObject;

    bool deactivatePointsOnExit;
    bool gazedInteractableCanvas;
    private void Awake()
    {
        if (interactivePointsParent)
            interactivePointsParent.SetActive(false);

        isOpen = false;

        if (startOn) return;
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.Instance.OnAnyObjectGazed += OnAnyObjectGazed;
        GameManager.Instance.OnTeleportEnd += OnTeleportEnded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAnyObjectGazed -= OnAnyObjectGazed;
        GameManager.Instance.OnTeleportEnd -= OnTeleportEnded;
    }

    public void OnPointerClick()
    {

    }

    public void OnGazedInteractableCanvas()
    {
        gazedInteractableCanvas = true;
    }

    public void OnLeftInteractableCanvasGazeWithDelay()
    {
        gazedInteractableCanvas = false;
        if (!myObject.activeInHierarchy)
        {
            return;
        }
        if (isPointerOnObject)
        {
            deactivatePointsOnExit = true;
            return;
        }
        if (isOpen)
        {
            DeactivateInteractablePoints();
        }

        
    }

    public void OnPointerEnter()
    {
        if (!myObject.activeInHierarchy)
        {
            return;
        }
        isPointerOnObject = true;
        deactivatePointsOnExit = false;
        ActivateInteractablePoints();
        GameManager.Instance.Fire_OnAnyObjectGazed(this);
    }

    public void OnPointerExit()
    {
        if (!myObject.activeInHierarchy)
        {
            return;
        }
        isPointerOnObject = false;
        if (isOpen && deactivatePointsOnExit)
        {
            DeactivateInteractablePoints();
            deactivatePointsOnExit = false;
        }
        else if (!deactivatePointsOnExit)
        {
            Invoke("DeactivateInteractablePoints", 1f);
        }

    }


    void OnTeleportEnded(Transform _playerTransform, bool _shouldApplyRotation)
    {
        if (interactivePointsParent.activeInHierarchy)
        {
            interactivePointsParent.SetActive(false);
        }
    }


    void OnAnyObjectGazed(InteractableObjectController _gazedObject)
    {
        if (_gazedObject != this && isOpen)
        {
            DeactivateInteractablePoints();
        }
    }

    void ActivateInteractablePoints()
    {
        if (interactivePointsParent)
            interactivePointsParent.SetActive(true);
        isOpen = true;

        if(animInteraction != null)
        {
            animInteraction.SetBool("Play", isOpen);
        }
    }

    void DeactivateInteractablePoints()
    {
        if (isPointerOnObject || gazedInteractableCanvas)
        {
            return;
        }
        if (interactivePointsParent)
            interactivePointsParent.SetActive(false);
        isOpen = false;

        if(animInteraction != null)
        {
            animInteraction.SetBool("Play", isOpen);
        }
    }
}
