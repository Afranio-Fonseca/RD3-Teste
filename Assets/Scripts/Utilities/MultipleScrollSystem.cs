using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(ScrollRect))]
public class MultipleScrollSystem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public List<ScrollRect> OtherScrollRect = new List<ScrollRect>();
    private ScrollRect _myScrollRect;
    //This tracks if the other one should be scrolling instead of the current one.
    private bool scrollOther;
    //This tracks wether the other one should scroll horizontally or vertically.
    private bool scrollOtherHorizontally;

    bool ready = false;

    private void Start()
    {
        Setup();
    }
    public void Setup()
    {
        //Get the current scroll rect so we can disable it if the other one is scrolling
        _myScrollRect = this.GetComponent<ScrollRect>();
        //If the current scroll Rect has the vertical checked then the other one will be scrolling horizontally.
        scrollOtherHorizontally = _myScrollRect.vertical;
        //Check some attributes to let the user know if this wont work as expected
        /*
        if (scrollOtherHorizontally)
        {
            if (_myScrollRect.horizontal)
                Debug.Log("You have added the SecondScrollRect to a scroll view that already has both directions selected");
            if (!OtherScrollRect.horizontal)
                Debug.Log("The other scroll rect doesnt support scrolling horizontally");
        }
        else if (!OtherScrollRect.vertical)
        {
            Debug.Log("The other scroll rect doesnt support scrolling vertically");
        }
        */


        ready = true;

    }
    //IBeginDragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!ready)
        {
            return;
        }
        //Get the absolute values of the x and y differences so we can see which one is bigger and scroll the other scroll rect accordingly
        float horizontal = Mathf.Abs(eventData.position.x - eventData.pressPosition.x);
        float vertical = Mathf.Abs(eventData.position.y - eventData.pressPosition.y);
        if (scrollOtherHorizontally)
        {
            Debug.Log(" DRAG horizontal is : " + horizontal + " Vertical is: " + vertical);
            if (horizontal > vertical)
            {
                scrollOther = true;
                //disable the current scroll rect so it doesnt move.
                _myScrollRect.enabled = false;
                for (int i = 0; i < OtherScrollRect.Count; i++)
                {
                    OtherScrollRect[i].OnBeginDrag(eventData);
                }
                
            }
        }
        else if (vertical > horizontal)
        {
            scrollOther = true;
            //disable the current scroll rect so it doesnt move.
            _myScrollRect.enabled = false;
            for (int i = 0; i < OtherScrollRect.Count; i++)
            {
                OtherScrollRect[i].OnBeginDrag(eventData);
            }
        }
    }
    //IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!ready)
        {
            return;
        }
        Debug.Log("ENDED DRAG");
        if (scrollOther)
        {
            scrollOther = false;
            _myScrollRect.enabled = true;
            for (int i = 0; i < OtherScrollRect.Count; i++)
            {
                OtherScrollRect[i].OnEndDrag(eventData);
            }
        }
    }
    //IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        if (!ready)
        {
            return;
        }
        
        if (scrollOther)
        {
            for (int i = 0; i < OtherScrollRect.Count; i++)
            {
                OtherScrollRect[i].OnDrag(eventData);
            }
        }
    }
}