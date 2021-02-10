using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsSave
{
    #region PlayerPrefs keys
    //public const string QUALITY_INT = "graphics quality";
    //public const string WINDOW_INT = "window mode";
    //public const string RESOLUTION = "resolution";
    public const string VOLUME_F = "volume";
    #endregion

    private const bool DEBUG_ON = true;

    public void RestorePrefrences()
    {
        SetVolume(GetVolume());
    }

    public void SetVolume(float vol)
    {
        vol = Mathf.Clamp(vol, 0, 1);
        Debug.Log(vol);
        PlayerPrefs.SetFloat(VOLUME_F, vol);
        AudioListener.volume = vol;
    }

    public float GetVolume()
    {
        return Mathf.Clamp(PlayerPrefs.GetFloat(VOLUME_F, 1), 0, 1);
    }
}
