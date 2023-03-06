using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMenuModel : MonoBehaviour
{
    [SerializeField] Canvas vrMenuCanvas;

    public delegate void VRMenuEvent(bool value);
    public event VRMenuEvent OnFocus;
    public event VRMenuEvent OnMenuMode;

    private bool isMenuMode;

    private void Start()
    {
        StartCoroutine(GetCamera());
    }
    private IEnumerator GetCamera()
    {
        while (vrMenuCanvas.worldCamera == null)
        {
            vrMenuCanvas.worldCamera = Camera.main;
        }
        yield return null;
    }

    public void FireVRMenuFocus(bool _value)
    {
        OnFocus?.Invoke(_value);
    }

    public void FireVRMenuMode(bool _value)
    {
        if(_value == isMenuMode)
        {
            return;
        }
        isMenuMode = _value;
        OnMenuMode?.Invoke(_value);
    }
}
