using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimerScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    float timer;

    int sec;
    int min;

    private void Update()
    {
        timer += Time.deltaTime;

        sec = (int)(timer % 60);
        min = (int)(timer / 60) % 60;

        text.text = string.Format("{0:00}:{1:00}", min, sec);
    }

}
