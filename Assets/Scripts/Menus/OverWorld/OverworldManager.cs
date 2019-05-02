using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct OverworldData
{
    [Header("Selected Level UI Elements")]
    public Image mapPreview;
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI description;
    public Button launchButton;
    public Button cancelButton;

    // Constructor for the UI elements
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
    public static OverworldManager instance; // Singleton instance
    public OverworldData data; // Reference to the struct containing the constructor
    PlayerController[] playerControllers; // For handling input

    // Enum for handling selected level states
    public enum Level
    {
        Level1,
        Level2,
        Level3
    };

    [Header("Level Management")]
    [Space] public Level level;
    int selectedLevel = 1;

    [Header("Ship Components")]
    public Transform shipTransform;
    
    [Header("Orbital Components")]
    public GameObject[] levelObjects;
    public GameObject[] orbitPositions;
    Vector3 shipPos; // Ship's current position at time of MoveShip() being called
    Vector3 shipDest; // Ship's target destination

    [Header("Overworld UI")]
    public GameObject levelPanel;
    public TextMeshProUGUI levelSelectedText;

    [Header("Selected Level UI Pool")]
    public Sprite[] mapImages;
    public string[] levelNames;
    [TextArea(3, 10)] public string[] descriptions;

    [Header("Orbit Settings")]
    [SerializeField] float travelTime = 1f;
    [SerializeField] float orbitSpeed = 80f;
    Vector3 direction = -Vector3.up; // The direction in which the ship orbits

    // These should be fairly self-explanatory 
    bool moving, orbiting;
    bool ableToLaunch;
    bool selecting;


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

    IEnumerator Start ()
    {
        ableToLaunch = false;

        playerControllers = FindObjectsOfType<PlayerController>();

        selectedLevel = 1;
        level = Level.Level1;

        MoveShip();
        ApplyText();

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < playerControllers.Length; i++)
        {
            playerControllers[i].gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
	
	void Update ()
    {       
        GetInput(); // Checks for input to select a level
        CheckIfOrbiting(); // Checks if the ship is supposed to be orbiting
        selectedPlanet();
    }

    // See Update() for explanation
    void CheckIfOrbiting()
    {
        if (orbiting)
        {
            OrbitShip(direction);
        }
    }

    // Rotates the ship around the selected level object in a given direction based on movement
    void OrbitShip(Vector3 dir)
    {
        shipTransform.RotateAround(levelObjects[selectedLevel - 1].transform.position, dir, orbitSpeed * Time.deltaTime);
    }

    void selectedPlanet()
    {
        for(int i = 0; i < levelObjects.Length; i++)
        {
            if(i == selectedLevel - 1)
            {
                levelObjects[i].gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                levelObjects[i].gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    // See Update() for explanation
    void GetInput()
    {
        // Checks for input from every player
        foreach (PlayerController player in playerControllers)
        {
            player.getInput(); // Checks for input from each player's PlayerController script

            // Selection input
            if (player.pickUp && !ableToLaunch && !selecting)
            {
                selecting = true;
                StartCoroutine(SelectionDelay());

                // Opens the mission panel UI
                SelectLevel();
            }
            else if (player.pickUp && ableToLaunch && !selecting)
            {
                selecting = true;
                StartCoroutine(SelectionDelay());

                // Clicks the "LAUNCH" button on the mission panel (starts the level)
                if (data.launchButton.interactable)
                {
                    data.launchButton.onClick.Invoke();
                }
            }

            // Cancelation input
            if (player.sprint && !selecting)
            {
                selecting = true;
                StartCoroutine(SelectionDelay());

                // Clicks the "CANCEL" button on the mission panel (cancels selection)
                data.cancelButton.onClick.Invoke();
            }

            // Directional movement input (RIGHT)
            if (player.movementVector.x > 0 && !moving)
            {
                /// SUMMARY: If the player moves to another level, data needs to be updated
                switch (selectedLevel)
                {
                    // If level 1 had been selected...
                    case 1:
                        selectedLevel++;
                        level = Level.Level2;
                        direction = -Vector3.up;
                        break;
                    // If level 2 had been selected...
                    case 2:
                        selectedLevel++;
                        level = Level.Level3;
                        direction = -Vector3.up;
                        break;
                    // If level 3 had been selected...
                    case 3:
                        selectedLevel = 1;
                        level = Level.Level1;
                        direction = Vector3.up;
                        break;
                }

                // Moves the ship and updates the UI according to the new selected level
                MoveShip();
                ApplyText();
            }

            // Directional movement input (LEFT)
            if (player.movementVector.x < 0 && !moving)
            {
                /// SUMMARY: If the player moves to another level, data needs to be updated
                switch (selectedLevel)
                {
                    // If level 1 had been selected...
                    case 1:
                        selectedLevel = 3;
                        level = Level.Level3;
                        direction = -Vector3.up;
                        break;
                    // If level 2 had been selected...
                    case 2:
                        selectedLevel--;
                        level = Level.Level1;
                        direction = Vector3.up;
                        break;
                    // If level 3 had been selected...
                    case 3:
                        selectedLevel--;
                        level = Level.Level2;
                        direction = Vector3.up;
                        break;
                }

                // Moves the ship and updates the UI according to the new selected level
                MoveShip();
                ApplyText();
            }
        }
    }

    // Opens the mission panel UI
    public void SelectLevel()
    {
        ableToLaunch = true;
        levelPanel.SetActive(true);

        // Creates a new instance of the mission panel constructor
        OverworldData selectionPanel = new OverworldData(data.mapPreview, data.levelName, data.description, data.launchButton, data.cancelButton);

        // Switches the UI information depending on which level is selected
        switch (level)
        {
            // If it's level 1, set all UI elements to the first item in each array pool
            case Level.Level1:
                selectionPanel.mapPreview.sprite = mapImages[0];
                selectionPanel.levelName.text = levelNames[0];
                selectionPanel.description.text = descriptions[0];
                selectionPanel.launchButton.interactable = true;
                break;
            // If it's level 2, set all UI elements to the second item in each array pool
            case Level.Level2:
                selectionPanel.mapPreview.sprite = mapImages[1];
                selectionPanel.levelName.text = levelNames[1];
                selectionPanel.description.text = descriptions[1];
                selectionPanel.launchButton.interactable = true;
                break;
            // If it's level 3, set all UI elements to the third item in each array pool
            case Level.Level3:
                selectionPanel.mapPreview.sprite = mapImages[2];
                selectionPanel.levelName.text = levelNames[2];
                selectionPanel.description.text = descriptions[2];
                selectionPanel.launchButton.interactable = false;
                break;
        }
    }

    // Closes the mission panel UI
    public void DeactivatePanel()
    {
        ableToLaunch = false;
        levelPanel.SetActive(false);
    }

    // Moves the ship to a new level
    void MoveShip()
    {
        DeactivatePanel();

        orbiting = false;
        moving = true;

        // Gets the ship's current position and orbit position of the next level
        shipPos = shipTransform.position;
        shipDest = orbitPositions[selectedLevel - 1].transform.position;

        // Starts the moving animation
        StartCoroutine(WaitAndMove());
    }

    // Basically just lerps the ship from it's current position to it's desired orbit position
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

    // Updates primary Overworld UI
    void ApplyText()
    {
        levelSelectedText.text = "Level " + selectedLevel.ToString();
    }

    IEnumerator SelectionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}
