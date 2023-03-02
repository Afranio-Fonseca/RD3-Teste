using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CasterInput : MonoBehaviour
{
    private GameObject hitObject;

    Transform playerTransform;

#if UNITY_EDITOR
    private Vector3 mousePos;
    private float timer;
#endif

    private void Awake()
    {
        playerTransform = transform.parent;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            timer = 10;
            if(mousePos.x == 0 && mousePos.y == 0)
            {
                mousePos = Input.mousePosition;
            }
            Vector3 deltaPos = (mousePos - Input.mousePosition)*0.2f;
            playerTransform.transform.rotation = Quaternion.Euler(playerTransform.transform.localRotation.eulerAngles.x, playerTransform.transform.rotation.eulerAngles.y + (deltaPos.x * -1), 0);
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x - deltaPos.y, transform.localRotation.eulerAngles.y, 0);
            //transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.rotation.eulerAngles.y+deltaPos.x, 0);
            mousePos = Input.mousePosition;
        }
        else
        {
            mousePos = new Vector3(0, 0,0);
        }
        timer += Time.deltaTime;
#endif

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 50))
        {
            if (hitObject != hit.transform.gameObject)
            {
                // New GameObject.
                hitObject?.GetComponent<IInputListener>()?.OnPointerExit();
                hitObject = hit.transform.gameObject;
                hitObject?.GetComponent<IInputListener>()?.OnPointerEnter();
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            hitObject?.GetComponent<IInputListener>()?.OnPointerExit();
            hitObject = null;
        }

        
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (timer < 4f)
        {
            GUI.Label(new Rect(Screen.width / 2f - 100f, Screen.height / 2f - 25f, 200f, 50f), "Keep Pressing ALT + MOVE MOUSE to simulate head tracking");
        }
    }

#endif
}
