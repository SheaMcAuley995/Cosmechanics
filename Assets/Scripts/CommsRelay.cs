using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommsRelay : MonoBehaviour
{
    public GameObject winScreen;
    public float currentProgress;
    public float maximumProgress;

    public void UploadSignal(bool isCharging)
    {
        if (isCharging)
        {
            currentProgress += Time.fixedDeltaTime;
            if (currentProgress >= maximumProgress)
            {
                WinGame();
            }
        }
    }

    void WinGame()
    {
        // TODO: Make prettier and animate
        winScreen.SetActive(true);
    }
}
