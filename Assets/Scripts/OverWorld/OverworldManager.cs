using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

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
    public OverworldData data;

    #region Rewired
    [HideInInspector] public int playerId = 0;
    Player[] players;
    ExampleGameController controller;
    #endregion

    public enum Level
    {
        Level1,
        Level2,
        Level3
    };
    [Space] public Level level;

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

    bool input, revInput, selectionInput, cancelInput, ableToLaunch;

    int selectedLevel = 1;

    bool moving, orbiting;
    Vector3 direction = -Vector3.up;

	// Use this for initialization
	void Start ()
    {
        ableToLaunch = false;

        controller = ExampleGameController.instance;

        players = new Player[controller.numberOfPlayers];
        for (int playerNumber = 0; playerNumber < controller.numberOfPlayers; playerNumber++)
        {
            players[playerNumber] = ReInput.players.GetPlayer(playerId);
        }

        selectedLevel = 1;
        level = Level.Level1;
        MoveShip();
        ApplyText();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
        ApplyInput();

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
        // For all player input
        foreach (Player player in players)
        {
            input = player.GetButtonDown("Move Horizontal");
            revInput = player.GetNegativeButtonDown("Move Horizontal");
            selectionInput = player.GetButtonDown("PickUp");
            cancelInput = player.GetButtonDown("Sprint");
        }

        // For singular player input
        //input = players[0].GetButtonDown("Move Horizontal");
        //revInput = players[0].GetNegativeButtonDown("Move Horizontal");
        //selectionInput = players[0].GetButtonDown("PickUp");
        //cancelInput = players[0].GetButtonDown("Sprint");
        //launchInput = players[0].GetButtonDown("Interact");
    }

    void ApplyInput()
    {
        if (input && !moving)
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

        if (revInput && !moving)
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

        if (selectionInput && !ableToLaunch)
        {
            SelectLevel();
        }
        else if (selectionInput && level == Level.Level1 && ableToLaunch)
        {
            data.launchButton.onClick.Invoke();
        }

        if (cancelInput)
        {
            data.cancelButton.onClick.Invoke();
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
                selectionPanel.launchButton.interactable = false;
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
