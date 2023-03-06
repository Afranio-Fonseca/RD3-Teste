using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    private Outline outline;

    [SerializeField] private CanvasGroupFader popUpFader;

    void Start()
    {
        outline = GetComponent<Outline>();

        outline.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            outline.enabled = true;
            popUpFader.DoFadeIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = false;
            popUpFader.DoFadeOut();
        }
    }


}
