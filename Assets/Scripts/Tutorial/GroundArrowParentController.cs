using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundArrowParentController : MonoBehaviour
{
    [SerializeField] GameObject[] arrows;
    void Start()
    {
        if (GameManager.Instance.Fire_GetArrowOption())
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ActivateArrows(int index)
    {
        if (index > arrows.Length || index < 0)
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].SetActive(false);
            }
            return;
        }

        for (int i = 0; i < arrows.Length; i++)
        {
            if(i == index)
            {
                arrows[i].SetActive(true);
            }
            else
            {
                arrows[i].SetActive(false);
            }
        }
    }
}
