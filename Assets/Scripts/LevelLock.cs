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
    public int earnedStars;
}


public class LevelLock : MonoBehaviour
{
    public static LevelLock instance;

    public RawImage displayImage;
    public List<LevelButton> levelList = new List<LevelButton>();

    [Header("Stars")]
    public Image[] starDisplay = new Image[3];
    public Sprite emptyStarTex;
    public Sprite filledStarTex;

    //public bool debugUnlockAllLevels;


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
            // Display level preview image.
            if (EventSystem.current.currentSelectedGameObject == levelButton.button.gameObject && levelButton.button.enabled == true)
            {
                displayImage.texture = levelButton.selectImage; 
            }

            // Display earned stars.
            if (EventSystem.current.currentSelectedGameObject == levelButton.button.gameObject)
            {
                switch (levelButton.earnedStars)
                {
                    case 0:
                        starDisplay[0].sprite = emptyStarTex;
                        starDisplay[1].sprite = emptyStarTex;
                        starDisplay[2].sprite = emptyStarTex;
                        break;
                    case 1:
                        starDisplay[0].sprite = filledStarTex;
                        starDisplay[1].sprite = emptyStarTex;
                        starDisplay[2].sprite = emptyStarTex;
                        break;
                    case 2:
                        starDisplay[0].sprite = filledStarTex;
                        starDisplay[1].sprite = filledStarTex;
                        starDisplay[2].sprite = emptyStarTex;
                        break;
                    case 3:
                        starDisplay[0].sprite = filledStarTex;
                        starDisplay[1].sprite = filledStarTex;
                        starDisplay[2].sprite = filledStarTex;
                        break;
                    default:
                        goto case 0;
                }
            }
        }
    }
}