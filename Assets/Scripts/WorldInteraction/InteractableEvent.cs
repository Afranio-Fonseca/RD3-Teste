using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableEvent : InteractableInput
{

    public UnityEvent OnPointerEnterEvent;
    public UnityEvent OnPointerExitEvent;
    public UnityEvent OnPointerClickEvent;

    

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
    }
}
