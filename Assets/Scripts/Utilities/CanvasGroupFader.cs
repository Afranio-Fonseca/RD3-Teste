using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] bool startOn;
	[SerializeField] float fadeTime = 0.01f;
	[Space]
	[SerializeField] UnityEvent OnFadeIn;
	[SerializeField] UnityEvent OnFadeOut;

	CanvasGroup myCanvasGroup;

    bool isFading;
    bool isFadedIn;
    float currentFadeTime;
    float percOfFade;
	
    void Awake()
    {       
        myCanvasGroup = this.GetComponent<CanvasGroup>();
        isFading = false;
        isFadedIn = false;

		SetCanvasFaded(startOn);
	}

    //private void Start()
    //{
    //    SetCanvasFaded(startOn);
    //}

    public void ToggleFade()
    {
        if (isFading) return;

        if (isFadedIn) StartCoroutine(FadeOut());
        else StartCoroutine(FadeIn());
    }

    public void DoFadeIn(bool forceFade = false)
    {
        if (!forceFade && (isFading || isFadedIn)) return;
		
        if(forceFade && isFading)
        {
            StopAllCoroutines();
            SetCanvasFaded(true);
        }
        else StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        currentFadeTime = 0f;
        percOfFade = 0f;

        while (isFading)
        {
            yield return null;

            currentFadeTime += Time.deltaTime;
            if (currentFadeTime > fadeTime)
            {
                currentFadeTime = fadeTime;
                isFading = false;
            }

            percOfFade = currentFadeTime / fadeTime;
            myCanvasGroup.alpha = Mathf.Lerp(0, 1, percOfFade);
        }

		OnFadeIn.Invoke();
        isFadedIn = true;
        myCanvasGroup.interactable = true;
        myCanvasGroup.blocksRaycasts = true;
    }

    public void DoFadeOut(bool forceFade = false)
    {
        if (!forceFade && (isFading || !isFadedIn)) return;
		
		if (forceFade && isFading)
        {
            StopAllCoroutines();
            SetCanvasFaded(false);
        }
        else StartCoroutine(FadeOut());
    }


    IEnumerator FadeOut()
    {
        isFading = true;
        currentFadeTime = 0f;
        percOfFade = 0f;

        myCanvasGroup.interactable = false;
        myCanvasGroup.blocksRaycasts = false;

        while (isFading)
        {
            yield return null;

            currentFadeTime += Time.deltaTime;
            if (currentFadeTime > fadeTime)
            {
                currentFadeTime = fadeTime;
                isFading = false;
            }

			isFadedIn = false;
            percOfFade = currentFadeTime / fadeTime;
            myCanvasGroup.alpha = Mathf.Lerp(1, 0, percOfFade);
		}

		OnFadeOut.Invoke();
	}

    public void SetCanvasFaded(bool fadedIn)
    {
        if(!myCanvasGroup) myCanvasGroup = this.GetComponent<CanvasGroup>();
        myCanvasGroup.alpha = (fadedIn) ? 1 : 0;
        myCanvasGroup.interactable = fadedIn;
        myCanvasGroup.blocksRaycasts = fadedIn;
        isFadedIn = fadedIn;
        isFading = false;

		if(isFadedIn)
			OnFadeIn.Invoke();
		else
			OnFadeOut.Invoke();
	}

	public void SetInteractable(bool isInteractable)
	{
		myCanvasGroup.interactable = isInteractable;
		myCanvasGroup.blocksRaycasts = isInteractable;
	}

    public bool GetIsFading()
    {
        return isFading;
    }

	public bool GetIsFadedIn()
	{
		return isFadedIn;
	}

	public void SetAlpha(float newAlpha)
	{
		if (newAlpha >= 1)
		{
			newAlpha = 1;
			SetInteractable(true);
		}
		else if (newAlpha <= 0)
		{
			newAlpha = 0;
			SetInteractable(false);
		}

		myCanvasGroup.alpha = newAlpha;
	}
}