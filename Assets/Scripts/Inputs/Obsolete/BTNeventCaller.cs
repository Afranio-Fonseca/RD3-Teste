using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BTNeventCaller : MonoBehaviour, IInputEvent
{
    [SerializeField]string localBtnID;
    [SerializeField] UnityEvent action;

    public string btnID
    {
        get 
        {
            return localBtnID;
        }
        set
        {
            localBtnID = value;
        }
    }

    public void HitMe()
    {
        Debug.Log("botao");
        action.Invoke();
    }

}
