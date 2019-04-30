using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AlarmStatus { Safe, Caution, Critical }
public class AlertUI : MonoBehaviour {


    [SerializeField] Color[] colors;
    [SerializeField] Image image;

    AlarmStatus alarmStatus;

    public float problemCurrent;
    public float problemMax;
    public AnimationCurve curve;

	void Start () {
        if(image == null)
        {
            image = GetComponent<Image>();
        }
        startingScalex = lightingImage.rectTransform.localScale.x;
        startingScaley = lightingImage.rectTransform.localScale.y;
    }
    public void Update()
    {
        setCurrentAlarmStatus();
       // Debug.Log("Alarm status :" + (int)alarmStatus);
       // Debug.Log("Problem Percentage %" + curve.Evaluate(problemPercentage()));
    }

    public void setCurrentAlarmStatus()
    {
        if(curve.Evaluate(problemPercentage()) * 100 < 25)
        {
            alarmStatus = AlarmStatus.Critical;
        }
        else if(curve.Evaluate(problemPercentage()) * 100 < 50)
        {
            alarmStatus = AlarmStatus.Caution;
        }
        else if(curve.Evaluate(problemPercentage()) * 100 >= 75)
        {
            alarmStatus = AlarmStatus.Safe;
        }

        image.color = colors[(int)alarmStatus];
    }

    public float problemPercentage()
    {
        return (problemCurrent / problemMax);
    }
}

        startingScalex = lightingImage.rectTransform.localScale.x;
        startingScaley = lightingImage.rectTransform.localScale.y;
    }