using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AlarmStatus { Safe, Caution, Critical }
public class AlertUI : MonoBehaviour {

    [SerializeField] Color[] colors;
    [SerializeField] Image image;
    [SerializeField] Image lightingImage;
    [SerializeField] float lightSize;
    [SerializeField] float lightSpeed;
    private float startingScalex;
    private float startingScaley;
    AlarmStatus alarmStatus;

    [HideInInspector] public float problemCurrent;
    [HideInInspector] public float problemMax;
    public AnimationCurve curve;

    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
        startingScalex = lightingImage.rectTransform.localScale.x;
        startingScaley = lightingImage.rectTransform.localScale.y;
    }
    public void Update()
    {
        setCurrentAlarmStatus();

    }

    public void setCurrentAlarmStatus()
    {
        if (curve.Evaluate(problemPercentage()) * 100 < 25)
        {
            alarmStatus = AlarmStatus.Critical;
        }
        else if (curve.Evaluate(problemPercentage()) * 100 < 50)
        {
            alarmStatus = AlarmStatus.Caution;
        }
        else if (curve.Evaluate(problemPercentage()) * 100 >= 75)
        {
            alarmStatus = AlarmStatus.Safe;
        }

        image.color = colors[(int)alarmStatus];
        lightingImage.color = new Color(colors[(int)alarmStatus].r, colors[(int)alarmStatus].g, colors[(int)alarmStatus].b, lightingImage.color.a);

        float x = Mathf.Abs(lightSize * Mathf.Sin(Time.timeSinceLevelLoad * lightSpeed) + startingScalex);
        //float y = lightSize * Mathf.Sin(Time.timeSinceLevelLoad);
        float y = Mathf.Abs(lightSize * Mathf.Sin(Time.timeSinceLevelLoad * lightSpeed) + startingScaley);
        lightingImage.rectTransform.localScale = new Vector3(x, y);
    }

    public float problemPercentage()
    {
        return (problemCurrent / problemMax);
    }
}
