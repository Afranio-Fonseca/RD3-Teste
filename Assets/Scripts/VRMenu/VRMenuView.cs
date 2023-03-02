using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMenuView : MonoBehaviour
{
    VRMenuModel vRMenuModel;

    [SerializeField] GameObject menuBTN;
    [SerializeField] GameObject menuFull;

    private void Awake()
    {
        vRMenuModel = FindObjectOfType<VRMenuModel>();
    }
    private void OnEnable()
    {
        vRMenuModel.OnMenuMode += OnMenuMode;
    }
    private void OnDisable()
    {
        vRMenuModel.OnMenuMode -= OnMenuMode;
    }

    void OnMenuMode(bool _value)
    {
        menuBTN.SetActive(!_value);
        menuFull.SetActive(_value);
    }
}
