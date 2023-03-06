using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SamambaiaInteraction : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private UnityEvent onParticleStop;

    private bool interact;

    private void Start()
    {
        interact = true;
    }

    void Update()
    {
        if(interact)
        {
            if (particle.isStopped)
            {
                onParticleStop.Invoke();

                interact = false;

            }
        }
    }
}
