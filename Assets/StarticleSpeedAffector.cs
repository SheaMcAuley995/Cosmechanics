using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarticleSpeedAffector : MonoBehaviour {

    ParticleSystem starticle;
    public float particleSpeed;

    private void Start()
    {
        starticle = GetComponent<ParticleSystem>();
      
    }
    private void Update()
    {
        var main = starticle.main;
        main.simulationSpeed
            = (Engine.instance.engineHeat/Engine.instance.maxHeat) * particleSpeed;
    }

}
