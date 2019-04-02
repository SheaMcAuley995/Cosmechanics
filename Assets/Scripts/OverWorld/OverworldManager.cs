using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class OverworldManager : MonoBehaviour
{
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
    public Level level;

    [Header("Components")]
    public GameObject[] levelObjects;
    public GameObject[] orbitPositions;
    public Transform shipTransform;
    Vector3 shipPos;
    Vector3 shipDest;

    [Header("UI")]
    public TextMeshProUGUI levelSelectedText;
    public GameObject[] levelPanels;

    [Header("Settings")]
    [SerializeField] float travelTime = 1f;
    [SerializeField] float orbitSpeed = 80f;

    bool input, revInput, selectionInput, cancelInput, launchInput;

    int selectedLevel = 1;

    bool moving, orbiting;
    Vector3 direction = -Vector3.up;

	// Use this for initialization
	void Start ()
    {
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
        input = players[0].GetButtonDown("Move Horizontal");
        revInput = players[0].GetNegativeButtonDown("Move Horizontal");
        selectionInput = players[0].GetButtonDown("PickUp");
        cancelInput = players[0].GetButtonDown("Sprint");
        launchInput = players[0].GetButtonDown("Interact");
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

        if (selectionInput)
        {
            SelectLevel();
        }

        if (cancelInput)
        {
            DeactivatePanels();
        }

        if (launchInput)
        {
            levelObjects[selectedLevel - 1].GetComponent<LevelSelectManager>().LaunchLevel("ZachNewMichaelTest");
        }
    }

    void SelectLevel()
    {
        switch (level)
        {
            case Level.Level1:
                levelPanels[0].SetActive(true);
                levelPanels[1].SetActive(false);
                levelPanels[2].SetActive(false);
                break;
            case Level.Level2:
                levelPanels[0].SetActive(false);
                levelPanels[1].SetActive(true);
                levelPanels[2].SetActive(false);
                break;
            case Level.Level3:
                levelPanels[0].SetActive(false);
                levelPanels[1].SetActive(false);
                levelPanels[2].SetActive(true);
                break;
        }
    }

    public void DeactivatePanels()
    {
        for (int i = 0; i < levelPanels.Length; i++)
        {
            levelPanels[i].SetActive(false);
        }
    }

    void MoveShip()
    {
        DeactivatePanels();

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
