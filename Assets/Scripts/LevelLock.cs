using Rewired.UI.ControlMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class LevelButton
{
    public Button button;
    public bool locked;
    public RawImage lockImage;
    public Texture selectImage;
}


public class LevelLock : MonoBehaviour
{
    public static LevelLock instance;

    public RawImage displayImage;
    public List<LevelButton> levelList = new List<LevelButton>();


    private void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        #endregion

        SaveLoadIO saveSystem = new SaveLoadIO(false); // Creates a new SaveLoadIO object. False argument tells constructor not to get level name.
        saveSystem.LoadUnlockStatus(); // Loads level unlock data from file into the list.
    }

    private void Start()
    {
        // MOVE THIS TO SEPARATE FUNCTION
        foreach (LevelButton levelButton in levelList)
        {
            levelButton.lockImage.enabled = levelButton.locked;

            if (levelButton.locked == true)
            {
                levelButton.button.enabled = false;
            }
            else
            {
                levelButton.button.enabled = true;
            }
        }
    }


    public void SwapImage()
    {
        foreach (LevelButton levelButton in levelList)
        {
            if (EventSystem.current.currentSelectedGameObject == levelButton.button.gameObject && levelButton.button.enabled == true)
            {
                displayImage.texture = levelButton.selectImage; 
            }
        }
    }
}