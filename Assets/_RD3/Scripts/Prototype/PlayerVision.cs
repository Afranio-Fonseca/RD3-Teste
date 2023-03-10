using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerVision : MonoBehaviour
{
    public float sensitivity = 1;

    public Transform head;

    public CursorLockMode lockMode;

    Vector3 rotationHead = Vector3.zero;

    void Update()
    {
        //Rotaca??o do corpo
        Vector3 rotationBody = transform.localEulerAngles;
        rotationBody.y += Input.GetAxis("Mouse X") * sensitivity;
        transform.localEulerAngles = rotationBody;

        //Rotata??o da cabe?a
        rotationHead.x -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationHead.x = Mathf.Clamp(rotationHead.x,-60, 60);
        head.localEulerAngles = rotationHead;

        Cursor.lockState = lockMode;      
    }
}
