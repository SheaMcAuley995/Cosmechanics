using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class SettingsMenu : MonoBehaviour 
{
    public Slider volSlider;
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;

    Resolution[] resolutions;

    List<Resolution> resList;
    
    public Dropdown qualityDropdown;
    public Dropdown fullscreenDropdown;

    const string prefName = "optionvalue";
    const string screenName = "screenvalue";
    const string resName = "resolutionoption";
    

    private void Awake()
    {
        resolutionDropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(resName, resolutionDropdown.value);
            PlayerPrefs.Save();
        }));

        qualityDropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(prefName, qualityDropdown.value);
            PlayerPrefs.Save();
        }));

        fullscreenDropdown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(screenName, fullscreenDropdown.value);
            PlayerPrefs.Save();
        }));
    }

    public void Start()
    {
        
        volSlider.value = PlayerPrefs.GetFloat("MVolume", 1f);
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("MVolume"));

        fullscreenDropdown.value = PlayerPrefs.GetInt(screenName, 0);
        qualityDropdown.value = PlayerPrefs.GetInt(prefName, 3);        

        resolutions = Screen.resolutions.Distinct().OrderBy(x => x.width).ThenBy(x => x.height).ThenBy(x => x.refreshRate) .ToArray();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        resList = new List<Resolution>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == 30 || resolutions[i].refreshRate == 60 || resolutions[i].refreshRate == 120 || resolutions[i].refreshRate == 144)
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
        resolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("MVolume", volume);
        audioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("MVolume"));
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
                PlayerPrefs.SetInt(screenName, 0);
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                PlayerPrefs.SetInt(screenName, 1);
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                PlayerPrefs.SetInt(screenName, 2);
                break;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
