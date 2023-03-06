using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalUIInteration : MonoBehaviour
{
    [SerializeField] IInputEvent[] inputEvents = new IInputEvent[0];

    private void Start()
    {
        inputEvents = FindObjectsOfType<MonoBehaviour>().OfType<IInputEvent>().ToArray();
    }

    public void FireInputEvent(string btnID)
    {
        inputEvents = FindObjectsOfType<MonoBehaviour>().OfType<IInputEvent>().ToArray();
        foreach (var item in inputEvents)
        {
            if (item.btnID == btnID)
            {
                item.HitMe();
            }
        }
    }
}
