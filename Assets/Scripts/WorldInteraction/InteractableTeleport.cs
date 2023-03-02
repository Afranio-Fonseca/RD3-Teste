using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableTeleport : InteractableInput
{
    [SerializeField] Transform teleportPoint;
    [SerializeField] Transform nextTeleportPoint;
    [SerializeField] Transform previousTeleportPoint;
    [SerializeField] GameObject eventGameObject;
    public UnityEvent OnTeleportEnd;

    [SerializeField] bool shouldApplyRotation = false;
    public override void OnPointerEnter()
    {
        base.OnPointerEnter();

    }
    public override void OnPointerExit()
    {
        base.OnPointerExit();

    }
    public override void OnPointerClick()
    {
        base.OnPointerClick();
        if (nextTeleportPoint != null)
        {
            GameManager.Instance.Fire_ArrowCheck(nextTeleportPoint);
        }
        else 
        {
            GameManager.Instance.Fire_OnArrowActive(false);
        }
        GameManager.Instance.Fire_OnTeleportStart(teleportPoint, shouldApplyRotation);
        GameManager.Instance.Fire_OnTeleportCheck(previousTeleportPoint, nextTeleportPoint);
        GameManager.Instance.Fire_OnEventCheck(eventGameObject);
        OnTeleportEnd?.Invoke();
    }
}
