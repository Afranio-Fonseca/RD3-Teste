using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowController : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] Animator anim;
    bool useThisArrow;
    public Transform target;

    private void Start()
    {
        useThisArrow = GameManager.Instance.Fire_GetArrowOption();
        if (useThisArrow)
        {
            GameManager.Instance.OnArrowCheck += SwitchTarget;
            GameManager.Instance.OnArrowActive += ControlArrow;
            anim.SetTrigger("TurnOn");
        }
        else
        {
            anim.SetTrigger("TurnOff");
        }
    }

    private void OnDisable()
    {
        if (useThisArrow)
        {
            GameManager.Instance.OnArrowCheck -= SwitchTarget;
            GameManager.Instance.OnArrowActive -= ControlArrow;
        }
    }

    void Update()
    {
        if (!useThisArrow) return;
        if (target == null) return;
        arrow.transform.LookAt(target);
    }

    public void SwitchTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ControlArrow(bool active)
    {
        if (active)
        {
            anim.SetTrigger("TurnOn");
        }
        else
        {
            Debug.Log("ARROW SET TRIGGER OFF");
            anim.SetTrigger("TurnOff");
        }
    }
}
