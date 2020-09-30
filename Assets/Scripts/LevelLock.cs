using Boo.Lang;
using Rewired.UI.ControlMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
    public class LevelButton
    {
        public Button button;
        public bool locked;
        public RawImage lockImage;
    }


public class LevelLock : MonoBehaviour
{
    public System.Collections.Generic.List<LevelButton> levelList = new System.Collections.Generic.List<LevelButton>();
    
    
    #region Save/Load
    [Serializable]
    struct SaveData
    {
        public System.Collections.Generic.List<LevelButton> list;
    }

    //save unlocked levels
    public object CaptureState()
    {
        return new SaveData
        {
            list = levelList
        };
    }

    //load unlocked levels
    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        
        if(saveData.list == null)
        {
            CaptureState();
        }

        levelList = saveData.list;
    }
    #endregion


    private void Start()
    {
        SaveData data;
        data.list = levelList;
        RestoreState(data);

        foreach (LevelButton levelButton in levelList)
        {
            levelButton.lockImage.enabled = levelButton.locked;

            if (levelButton.locked == true)
            {
                levelButton.button.interactable = false;
            }
            else
            {
                levelButton.button.interactable = true;
            }
        }
    }
}