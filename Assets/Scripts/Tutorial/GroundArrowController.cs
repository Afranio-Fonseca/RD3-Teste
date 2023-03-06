using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundArrowController : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Start()
    {
        if (target != null)
        {
            this.transform.LookAt(target);
        }
        else
        {
            DisableArrow();
        }
    }

    void DisableArrow()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = new Color(spriteRenderers[i].color.r, spriteRenderers[i].color.g, spriteRenderers[i].color.b, 0);
        }
    }
}
