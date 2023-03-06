using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaneClipController : MonoBehaviour
{
    [SerializeField]
    float fltBorderSize = 0.1f;
    [SerializeField]
    bool bolEndingPlane = false;

    private void Start() {
        this.gameObject.transform.localScale = new Vector3(1,1,1);
    }

    //void FixedUpdate()
    //{
    //    UpdateClippingPlane();
    //}


    void Update()
    {
        UpdateClippingPlane();
    }

    void UpdateClippingPlane()
    {
        if (!bolEndingPlane)
        {
            Shader.SetGlobalVector("Vector3_SectionPlane", transform.position + (transform.up * 100000));
            Shader.SetGlobalVector("Vector3_SectionBorder", transform.position - (transform.up * (fltBorderSize * this.gameObject.transform.lossyScale.x)));
            Shader.SetGlobalVector("Vector3_SectionPoint", transform.position);
        }
        else
        {
            Shader.SetGlobalVector("Vector3_EndingPlane", transform.position + (transform.up * 100000));
            Shader.SetGlobalVector("Vector3_EndingBorder", transform.position - (transform.up * (fltBorderSize * this.gameObject.transform.lossyScale.x)));
            Shader.SetGlobalVector("Vector3_EndingPoint", transform.position);
        }
    }
}
