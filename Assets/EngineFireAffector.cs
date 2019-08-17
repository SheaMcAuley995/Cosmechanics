using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineFireAffector : MonoBehaviour {

    ParticleSystem engineFire;
    public float particleEmissionRate;

    private void Start()
    {
        engineFire = GetComponent<ParticleSystem>();

    }
    private void Update()
    {
        var em = engineFire.emission;
        //var main = engineFire.main;
        em.rateOverTimeMultiplier = (Engine.instance.engineHeat / Engine.instance.maxHeat) * particleEmissionRate;
       // float heatlvl = Engine.instance.engineHeat / Engine.instance.maxHeat;
        //Debug.LogWarning(heatlvl + " heat level");
        //var startSpeed = main.startSpeed;
        //startSpeed.constant -= (Engine.instance.engineHeat / Engine.instance.maxHeat)/10;
        //main.startSpeed = startSpeed;
    }
}
