using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsRelay : MonoBehaviour
{
    public float currentProgress;
    public float levelTimer;

    public void UploadSignal(bool isCharging)
    {
        if (isCharging)
        {
            currentProgress += Time.fixedDeltaTime;
        }
    }
}
