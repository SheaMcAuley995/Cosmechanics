using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warningShipLights : MonoBehaviour {

    Image warningLight;
    float startingAlbedo;
    private void Start()
    {
        warningLight = GetComponent<Image>();
        startingAlbedo = warningLight.color.a;
    }
    // Update is called once per frame
    void Update () {

        if((Engine.instance.enemyProgress / Engine.instance.currentProgress) > 0.75f)
        warningLight.color = new Color(warningLight.color.r, warningLight.color.b, warningLight.color.g,Mathf.Sin(Time.timeSinceLevelLoad * (Engine.instance.enemyProgress/Engine.instance.currentProgress) * 10) + startingAlbedo);

    }
}
