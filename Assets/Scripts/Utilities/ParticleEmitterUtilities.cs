using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleEmitterUtilities : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] particles = new ParticleSystem[0];


    public void EmitParticles(string indexNvalues)
    {
        // indexNvalues >> "numero de index" + / + "numero de particulas"
        string[] values = indexNvalues.Split("/");

        int index = int.Parse(values[0]);
        int numberOfParticles = int.Parse(values[1]);

        particles[index].Emit(numberOfParticles);
    }

    public void ToggleEmission(int index)
    {
        particles[index].enableEmission = !particles[index].emission.enabled;
    }
}
