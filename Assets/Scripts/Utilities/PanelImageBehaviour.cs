using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelImageBehaviour : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    
    public void ShowImage()
    {
        if (canvasGroup.alpha == 1) return;
        StartCoroutine(Tween.TweenAlpha(canvasGroup, 1, 0, 1, 1));
    }

    public void HideImage()
    {
        if (canvasGroup.alpha == 0) return;
        StartCoroutine(Tween.TweenAlpha(canvasGroup, 0, 1, 1, 0));
    }
}
