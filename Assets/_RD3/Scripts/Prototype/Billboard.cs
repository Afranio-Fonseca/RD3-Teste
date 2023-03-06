using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        Transform target = GameObject.FindWithTag("Player").transform;
        transform.LookAt(target);
    }
}
