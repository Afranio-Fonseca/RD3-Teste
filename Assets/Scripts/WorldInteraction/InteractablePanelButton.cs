using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractablePanelButton : InteractableInput
{
    public UnityEvent OnPointerClickEvent;

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
        OnPointerClickEvent?.Invoke();
    }
}
