using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateUtilities : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    string speedMultiplierParam = "AnimSpeed";
    [SerializeField]
    float timerSpeed = 0.005f;
    [SerializeField]
    Gradient slowPermission = new Gradient();

    float timer_Current;
    bool slowed = false;
    bool canBeSlowed = true;
    float currentStateTime = 0;


    public void ToggleStateSpeed()
    {
        CheckSlowPermission();

        if(!slowed && canBeSlowed)
            SlowDownState();
        else if(slowed)
            SpeedUpState();
        else Invoke("ToggleStateSpeed", 0.2f);
    }

    void CheckSlowPermission()
    {
        currentStateTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        while(currentStateTime > 1)
        {
            currentStateTime -= 1;
        }

        if(slowPermission.Evaluate(currentStateTime) == Color.white)      
            canBeSlowed = true;      
        else  
            canBeSlowed = false;
    }

    private void SlowDownState()
    {
        StartCoroutine(SetSpeed_Routine(0f));
        slowed = true;
    }

    private void SpeedUpState()
    {
        StartCoroutine(SetSpeed_Routine(1f));
        slowed = false;
    }

    IEnumerator SetSpeed_Routine(float tarSpeed)
    {
        float speed = 0f;
        speed += anim.GetFloat(speedMultiplierParam);

        timer_Current = 0f;

        while (speed != tarSpeed)
        {
            timer_Current += Time.deltaTime;
            if (timer_Current > timerSpeed) timer_Current = timerSpeed;

            speed = Mathf.MoveTowards(speed, tarSpeed, timer_Current);

            anim.SetFloat(speedMultiplierParam, speed);

            yield return null;
        }
    }
}
