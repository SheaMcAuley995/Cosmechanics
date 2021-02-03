using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class SettingsMenu : MonoBehaviour 
{
    PlayerSettingsSave playerSettings;

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    List<Resolution> resList;

    public PlayerSettingsSave prefs { get; private set; }

    public void Start()
    {

        resolutions = Screen.resolutions.Distinct().OrderBy(x => x.width).ThenBy(x => x.height).ThenBy(x => x.refreshRate) .ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        resList = new List<Resolution>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == 30 || resolutions[i].refreshRate == 60)
            {
                resList.Add(resolutions[i]);
            }

        }

        for (int i = 0; i < resList.Count; i++)
        {
                string option = resList[i].width + " x " + resList[i].height + " @" + resList[i].refreshRate + "hz";

                options.Add(option);
                if (resList[i].width == Screen.currentResolution.width && resList[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                } 
            
        }


        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        //prefs.SetVolume(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen (int fullscreenIndex)
    {
        switch(fullscreenIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
