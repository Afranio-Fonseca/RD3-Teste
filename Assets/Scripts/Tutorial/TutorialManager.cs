using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject platforms;
    [SerializeField] Animator platformsAnim;

    public void FinishTutorial()
    {
        StartCoroutine(FinishTutorialRoutine());
    }

    IEnumerator FinishTutorialRoutine()
    {
        yield return null;
        GameManager.Instance.Fire_OnTutorialEnd();
        //GameManager.Instance.Fire_ArrowCheck(GameObject.Find("FirstPoint - FOUND BY CODE").GetComponent<Transform>());
        platforms.SetActive(false);
    }

    public void EnablePlatforms(GameObject point)
    {
        StartCoroutine(EnablePlatformsRoutine(point));
    }

    IEnumerator EnablePlatformsRoutine(GameObject point)
    {
        platformsAnim.SetTrigger("Trigger");
        yield return new WaitForSeconds(3);
        point.SetActive(true);
    }
}
