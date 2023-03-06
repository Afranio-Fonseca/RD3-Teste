using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    private void Start()
    {
        Quaternion baseRotation = new Quaternion(0, 0, 0, 0);
        rect.rotation = baseRotation;
    }
}
