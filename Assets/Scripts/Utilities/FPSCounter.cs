using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    int current;
    [SerializeField] TMP_Text fpsText;
    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;
    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            current = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + current.ToString();
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
        //Debug.Log("Current FPS is " + current);
    }
}
