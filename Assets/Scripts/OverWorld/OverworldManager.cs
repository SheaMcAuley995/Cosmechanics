using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct OverworldData
{
    public Image mapPreview;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI description;
    public Button launchButton;
    public Button cancelButton;

    public OverworldData(Image _mapPreview, TextMeshProUGUI _levelName, TextMeshProUGUI _description, Button _launchButton, Button _cancelButton)
    {
        mapPreview = _mapPreview;
        levelName = _levelName;
        description = _description;
        launchButton = _launchButton;
        cancelButton = _cancelButton;
    }
}

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager instance;

    public OverworldData data;

    PlayerController[] playerControllers;

    public enum Level
    {
        Level1,
        Level2,
        Level3
    };
    [Space] public Level level;
    int selectedLevel = 1;

    [Header("Class Components")]
    public Transform shipTransform;
    
    [Header("Orbital Components")]
    public GameObject[] levelObjects;
    public GameObject[] orbitPositions;
    Vector3 shipPos;
    Vector3 shipDest;

    [Header("Main UI")]
    public GameObject levelPanel;
    public TextMeshProUGUI levelSelectedText;

    [Header("Level UI Selection Pool")]
    public Sprite[] mapImages;
    public string[] levelNames;
    [TextArea(3, 10)] public string[] descriptions;

    [Header("Orbit Settings")]
    [SerializeField] float travelTime = 1f;
    [SerializeField] float orbitSpeed = 80f;
    Vector3 direction = -Vector3.up;

    bool moving, orbiting;
    bool ableToLaunch;


    #region Singleton
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    void Start ()
    {
        ableToLaunch = false;

        playerControllers = FindObjectsOfType<PlayerController>();

        selectedLevel = 1;
        level = Level.Level1;

        MoveShip();
        ApplyText();
    }
	
	void Update ()
    {
        GetInput();

        if (orbiting)
        {
            OrbitShip(direction);
        }
    }

    void OrbitShip(Vector3 dir)
    {
        shipTransform.RotateAround(levelObjects[selectedLevel - 1].transform.position, dir, orbitSpeed * Time.deltaTime);
    }

    void GetInput()
    {
        foreach (PlayerController player in playerControllers)
        {
            player.getInput();

            // Select input
            if (player.pickUp && !ableToLaunch)
            {
                SelectLevel();
            }
            else if (player.pickUp && ableToLaunch)
            {
                data.launchButton.onClick.Invoke();
            }

            // Cancel input
            if (player.sprint)
            {
                data.cancelButton.onClick.Invoke();
            }

            // Move input
            if (player.movementVector.x > 0 && !moving)
            {
                switch (selectedLevel)
                {
                    case 1:
                        selectedLevel++;
                        level = Level.Level2;
                        direction = -Vector3.up;
                        break;
                    case 2:
                        selectedLevel++;
                        level = Level.Level3;
                        direction = -Vector3.up;
                        break;
                    case 3:
                        selectedLevel = 1;
                        level = Level.Level1;
                        direction = Vector3.up;
                        break;
                }
                MoveShip();
                ApplyText();
            }

            if (player.movementVector.x < 0 && !moving)
            {
                switch (selectedLevel)
                {
                    case 1:
                        selectedLevel = 3;
                        level = Level.Level3;
                        direction = -Vector3.up;
                        break;
                    case 2:
                        selectedLevel--;
                        level = Level.Level1;
                        direction = Vector3.up;
                        break;
                    case 3:
                        selectedLevel--;
                        level = Level.Level2;
                        direction = Vector3.up;
                        break;
                }
                MoveShip();
                ApplyText();
            }
        }
    }

    public void SelectLevel()
    {
        ableToLaunch = true;
        levelPanel.SetActive(true);
        OverworldData selectionPanel = new OverworldData(data.mapPreview, data.levelName, data.description, data.launchButton, data.cancelButton);

        switch (level)
        {
            case Level.Level1:
                selectionPanel.mapPreview.sprite = mapImages[0];
                selectionPanel.levelName.text = levelNames[0];
                selectionPanel.description.text = descriptions[0];
                selectionPanel.launchButton.interactable = true;
                break;
            case Level.Level2:
                selectionPanel.mapPreview.sprite = mapImages[1];
                selectionPanel.levelName.text = levelNames[1];
                selectionPanel.description.text = descriptions[1];
                selectionPanel.launchButton.interactable = true;
                break;
            case Level.Level3:
                selectionPanel.mapPreview.sprite = mapImages[2];
                selectionPanel.levelName.text = levelNames[2];
                selectionPanel.description.text = descriptions[2];
                selectionPanel.launchButton.interactable = false;
                break;
        }
    }

    public void DeactivatePanel()
    {
        ableToLaunch = false;
        levelPanel.SetActive(false);
    }

    void MoveShip()
    {
        DeactivatePanel();

        orbiting = false;
        moving = true;

        shipPos = shipTransform.position;
        shipDest = orbitPositions[selectedLevel - 1].transform.position;

        StartCoroutine(WaitAndMove());
    }

    IEnumerator WaitAndMove()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= travelTime)
        {
            shipTransform.position = Vector3.Lerp(shipPos, shipDest, Time.time - startTime);

            yield return 1;
        }

        moving = false;
        orbiting = true;

        yield return null;
    }

    void ApplyText()
    {
        levelSelectedText.text = "Level " + selectedLevel.ToString();
    }
}
