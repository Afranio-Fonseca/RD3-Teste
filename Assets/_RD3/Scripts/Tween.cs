using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Tween
{
    public static IEnumerator TweenColor(SpriteRenderer sprite, Color from, Color to, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < duration)
        {
            percent += Time.deltaTime;
            sprite.color = Color.Lerp(from, to, percent / duration);
            yield return update;
        }

        sprite.color = to;
    }

    public static IEnumerator TweenColor(Image image, Color from, Color to, float duration, float delay = 0)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        if(delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        while (percent < duration)
        {
            percent += Time.deltaTime;
            image.color = Color.Lerp(from, to, percent / duration);

            yield return update;
        }

        image.color = to;
    }

    public static IEnumerator TweenColor(TextMeshProUGUI text, Color from, Color to, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < duration)
        {
            percent += Time.deltaTime;
            text.color = Color.Lerp(from, to, percent / duration);
            yield return update;
        }

        text.color = to;
    }

    public static IEnumerator TweenAlpha(CanvasGroup canvasGroup, float to, float from, float duration, float delay = 0)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        while (percent < duration)
        {
            percent += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, percent / duration);
            yield return update;
        }
    }

    public static IEnumerator TweenFill(Image image, float to, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while (percent < duration)
        {
            percent += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(image.fillAmount, to, percent / duration);
            yield return update;
        }
    }

    public static IEnumerator TweenMoveTowards(GameObject go, Vector2 to, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while(percent < duration)
        {
            percent += Time.deltaTime;
            go.transform.localPosition = Vector2.Lerp(go.transform.localPosition, to, percent / duration);
            yield return update;
        }
    }

    public static IEnumerator TweenScale(GameObject go, Vector3 to, float duration, System.Action OnFinish = null)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while(percent < duration)
        {
            percent += Time.deltaTime;
            go.transform.localScale = Vector3.Lerp(go.transform.localScale, to, percent / duration);
            yield return update;
        }

        if (OnFinish != null)
        {
            OnFinish();
        }
    }

    public static IEnumerator TweenLocalX(GameObject go, float to, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();
        Vector3 newXPosition = new Vector3(to, go.transform.localPosition.y, go.transform.localPosition.z);

        while(percent < duration)
        {
            percent += Time.deltaTime;
            go.transform.localPosition = Vector3.Lerp(go.transform.localPosition, newXPosition, percent / duration);
            yield return update;
        }
    }

    public static IEnumerator TweenRotate(GameObject go, Vector3 rotation, float duration)
    {
        float percent = 0;
        WaitForFixedUpdate update = new WaitForFixedUpdate();

        while(percent < duration)
        {
            percent += Time.deltaTime;
            go.transform.eulerAngles = Vector3.Lerp(go.transform.eulerAngles, rotation, percent / duration);
            yield return update;
        }
    }
}
