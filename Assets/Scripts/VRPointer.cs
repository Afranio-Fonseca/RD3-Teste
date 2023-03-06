using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRPointer : MonoBehaviour
{
    public Image overlay;
    float timer = 0;
    bool isTouch = false;
    public GameObject[] toOpen;
    public GameObject[] toClose;


    public void Update()
    {
        if (isTouch){
            timer += Time.deltaTime;
            overlay.fillAmount = (timer / 3f);
            if (timer >= 3f){
                OnPointerClick();
                timer = 0;
                isTouch = false;
            }
        }
        else
        {
            overlay.fillAmount = 0;
        }
    }
    public void OnPointerEnter()
    {
        isTouch = true;
        timer = 0;
    }

    public void OnPointerExit()
    {
        isTouch = false;
        timer = 0;
    }
    public void OnPointerClick()
    {
        for (int i = 0; i < toOpen.Length; i++)
        {
            toOpen[i].SetActive(true);
        }
        for (int i = 0; i < toClose.Length; i++)
        {
            toClose[i].SetActive(false);
        }
    }
}
