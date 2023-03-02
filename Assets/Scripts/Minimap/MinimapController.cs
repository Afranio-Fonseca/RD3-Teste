using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] GameObject[] points;

    public void MoveMinimapCursor(int index)
    {
        if(index > points.Length)
        {
            return;
        }
        
        for (int i = 0; i < points.Length; i++)
        {
            if(index == i)
            {
                points[i].SetActive(true);
            }
            else
            {
                points[i].SetActive(false);
            }
        }
    }
}