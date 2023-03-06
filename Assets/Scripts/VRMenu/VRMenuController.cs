using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRMenuController : MonoBehaviour, IInputListener
{
    private Transform cam;
    private VRMenuModel vRMenuModel;
    private bool isMenuMode;

    void Awake()
    {
        vRMenuModel = GetComponent<VRMenuModel>();
        cam = Camera.main.transform;
    }

    private void OnEnable()
    {
        vRMenuModel.OnMenuMode += OnMenuMode;
    }

    private void OnDisable()
    {
        vRMenuModel.OnMenuMode -= OnMenuMode;
    }

    void LateUpdate()
    {
        FollowCamera();
        if (!isMenuMode)
        {
            TrackXAxis();
            TrackYAxis();
        }

    }

    private void FollowCamera()
    {
        if (cam.transform.position != transform.position)
        {
            transform.position = cam.transform.position;
        }
    }

    private void TrackXAxis()
    {
        
    }

    private void TrackYAxis()
    {
        transform.rotation = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0);
    }


    public void OnPointerEnter()
    {
        //vRMenuModel.FireVRMenuMode(true);
    }
    public void OnPointerExit()
    {
        //vRMenuModel.FireVRMenuMode(false);
    }

    public void OnPointerClick()
    {

    }
    //Metodes Delegates
    void OnMenuMode(bool _value)
    {
        isMenuMode = _value;
    }

}
