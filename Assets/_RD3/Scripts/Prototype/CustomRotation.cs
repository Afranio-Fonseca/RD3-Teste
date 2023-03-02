using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomRotation : MonoBehaviour
{
    public bool overrideRotation;
    public Vector3 rotation;

    public float XAngle = 0;
    public float ZAngle = 0;
    void Start()
    {
        if(overrideRotation)
        {
            transform.eulerAngles = rotation;
        }
        else
        {
            transform.eulerAngles = new Vector3(XAngle, transform.eulerAngles.y, XAngle);
        }
    }
}
