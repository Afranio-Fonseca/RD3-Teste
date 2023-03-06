using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimGif : MonoBehaviour
{
    //[SerializeField] private Texture2D[] frames;
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float fps = 10.0f;

    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        int index = (int)(Time.time * fps);
        index = index % frames.Length;
        img.sprite = frames[index];
    }
}
