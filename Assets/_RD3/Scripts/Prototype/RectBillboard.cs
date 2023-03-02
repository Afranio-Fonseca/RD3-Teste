using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectBillboard : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    private void LateUpdate()
    {
        Transform target = GameObject.FindWithTag("Player").transform;
        rect.LookAt(target);
    }
}
