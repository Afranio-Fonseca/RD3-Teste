using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    Material material;
    bool isDissolving = false;
    public float speed = 1f;
    float fade = 0;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDissolving)
        {
            fade += Time.deltaTime * speed;

            if( fade >= 1)
            {
                fade = 1;
                isDissolving = false;
            }

            material.SetFloat("_DissolveAmount", fade);
        }
    }

    public void Dissolve()
    {
        fade = 0;
        isDissolving = true;
    }
}
