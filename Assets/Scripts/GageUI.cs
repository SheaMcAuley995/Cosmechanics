using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GageUI : MonoBehaviour {

    public float percentageValue;
    public RectTransform gageImage;
    //public Engine engine;
    public float offset;
	void Update () {
        gageImage.eulerAngles = new Vector3(0,0,-(((Engine.instance.engineHeatPercentage() / 100) * 245) + offset));
	}
}
