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
    public ShipController shipController; // For handling input
    PlayerController[] playerControllers;
    SelectedPlayer[] selectedPlayers;

    // Enum for handling selected level states
    public enum Level
    {
        Level1,
        Level2,
        Level3,
        Level4
    };

    [Header("Level Management")]
    [Space] public Level level;
    int selectedLevel = 1;
    public string charSelectSceneName;

    [Header("Ship Components")]
    public Transform shipTransform;
    
    [Header("Orbital Components")]
    public Image[] levelObjects;
    public GameObject[] orbitPositions;
    Vector3 shipPos; // Ship's current position at time of MoveShip() being called
    Vector3 shipDest; // Ship's target destination

    [Header("Overworld UI")]
    public GameObject levelPanel;
    public TextMeshProUGUI levelSelectedText;
    public Sprite[] highlightSprites;
    public TextMeshProUGUI launchButtonText;

    [Header("Selected Level UI Pool")]
    public Sprite[] mapImages;
    public string[] levelNames;
    [TextArea(3, 10)] public string[] descriptions;

    [Header("Orbit Settings")]
    [SerializeField] float travelTime = 1f;
    [SerializeField] float orbitSpeed = 80f;
    [SerializeField] Vector3 orbitOffset = new Vector3(15f, 0f, 10f);
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

    void Start ()
    {
        ableToLaunch = false;

        playerControllers = FindObjectsOfType<PlayerController>();

        selectedLevel = 1;
        level = Level.Level1;

        MoveShip();
        ApplyText();

        foreach (PlayerController player in playerControllers)
        {
            player.cameraTrans = Camera.main.transform;
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
                //levelObjects[i].gameObject.transform.localScale = new Vector3(1, 1, 1);
                levelObjects[i].sprite = highlightSprites[1];
            }
            else
            {
                //levelObjects[i].gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                levelObjects[i].sprite = highlightSprites[0];
                levelObjects[3].sprite = highlightSprites[3]; // THIS LOCKS SHIP 3 SINCE IT'S NOT FINISHED
            }
        }
    }

    // See Update() for explanation
    void GetInput()
    {
        shipController.GetInput(); // Checks for input from the ship

        // Selection input
        if (shipController.pickUp && !ableToLaunch && !selecting)
        {
            selecting = true;
            StartCoroutine(SelectionDelay());

            levelObjects[selectedLevel - 1].sprite = highlightSprites[2];

            // Opens the mission panel UI
            SelectLevel();
        }

        // Directional movement input (RIGHT)
        if (shipController.movementVector.x > 0 && !moving && !ableToLaunch)
        {
            // SUMMARY: If the player moves to another level, data needs to be updated
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
                    selectedLevel++;
                    level = Level.Level4;
                    direction = Vector3.up;
                    break;
                // If level 4 had been selected...
                case 4:
                    selectedLevel = 1;
                    level = Level.Level1;
                    direction = Vector3.up;
                    break;
            }

            // Moves the ship and updates the UI according to the new selected level
            MoveShip();
            DeactivatePanel();
            ApplyText();
        }

        // Directional movement input (LEFT)
        if (shipController.movementVector.x < 0 && !moving && !ableToLaunch)
        {
            /// SUMMARY: If the player moves to another level, data needs to be updated
            switch (selectedLevel)
            {
                // If level 1 had been selected...
                case 1:
                    selectedLevel = 4;
                    level = Level.Level4;
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
                case 4:
                    selectedLevel--;
                    level = Level.Level3;
                    direction = Vector3.up;
                    break;
            }

            // Moves the ship and updates the UI according to the new selected level
            MoveShip();
            DeactivatePanel();
            ApplyText();
        }

        if (shipController.sprint && !ableToLaunch && !selecting)
        {
            selectedPlayers = FindObjectsOfType<SelectedPlayer>();

            selecting = true;
            StartCoroutine(SelectionDelay());

            foreach (SelectedPlayer player in selectedPlayers)
            {
                //player.gameObject.AddComponent<CharToDestroy>();
                Destroy(player.gameObject);
            }
            SceneFader.instance.FadeTo(charSelectSceneName);
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
                launchButtonText.text = "Launch";
                break;
            // If it's level 2, set all UI elements to the second item in each array pool
            case Level.Level2:
                selectionPanel.mapPreview.sprite = mapImages[1];
                selectionPanel.levelName.text = levelNames[1];
                selectionPanel.description.text = descriptions[1];
                selectionPanel.launchButton.interactable = true;
                launchButtonText.text = "Launch";
                break;
            // If it's level 3, set all UI elements to the third item in each array pool
            case Level.Level3:
                selectionPanel.mapPreview.sprite = mapImages[2];
                selectionPanel.levelName.text = levelNames[2];
                selectionPanel.description.text = descriptions[2];
                selectionPanel.launchButton.interactable = true;
                launchButtonText.text = "Launch";
                break;
            // If it's level 4, set all UI elements to the fourth item in each array pool
            case Level.Level4:
                selectionPanel.mapPreview.sprite = mapImages[3];
                selectionPanel.levelName.text = levelNames[3];
                selectionPanel.description.text = descriptions[3];
                selectionPanel.launchButton.interactable = false;
                launchButtonText.text = "CANNOT LAUNCH";
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
        shipDest = levelObjects[selectedLevel - 1].transform.position + orbitOffset;

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
        if (selectedLevel == 1)
        {
            levelSelectedText.text = "Tutorial";
        }
        else
        {
            levelSelectedText.text = "Level " + (selectedLevel - 1).ToString();
        }
    }

    IEnumerator SelectionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        selecting = false;
        yield return null;
    }
}
