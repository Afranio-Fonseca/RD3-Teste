using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTeleportTutorial : InteractableInput
{
    public UnityEvent OnPointerEnterEvent;
    public UnityEvent OnPointerExitEvent;
    public UnityEvent OnPointerClickEvent;

    [SerializeField] bool shouldApplyRotation = false;
    [SerializeField] Transform teleportPoint;
    [SerializeField] Transform nextPoint;
    [SerializeField] GameObject myObject;

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
        OnPointerEnterEvent.Invoke();
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
        OnPointerExitEvent.Invoke();
    }

    public override void OnPointerClick()
    {
        base.OnPointerClick();
        OnPointerClickEvent.Invoke();
        GameManager.Instance.Fire_OnTeleportStart(teleportPoint, shouldApplyRotation);
        myObject.SetActive(false);
        
        if (nextPoint != null)
        {
            nextPoint.gameObject.SetActive(true);
        }
    }
}
