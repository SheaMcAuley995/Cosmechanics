using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Rewired;

public class OverworldManager : MonoBehaviour
{
    #region Rewired
    [HideInInspector] public int playerId = 0;
    Player[] players;
    ExampleGameController controller;
    #endregion

    [Header("Components")]
    public GameObject[] levelObjects;
    public Transform shipTransform;
    Vector3 shipPos;
    Vector3 shipDest;

    [Header("Settings")]
    [SerializeField] float travelTime = 1f;

    bool input;
    float _input;
    int selectedLevel = 1;

    public TextMeshProUGUI levelSelectedText;

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
        MoveShip();
        ApplyText();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
        ApplyInput();
	}

    void GetInput()
    {
        input = players[0].GetButtonDown("PickUp");
        _input = players[0].GetAxisRaw("Move Horizontal");
    }

    void ApplyInput()
    {
        if (_input > 0)
        {
            switch (selectedLevel)
            {
                case 1:
                    selectedLevel++;
                    MoveShip();
                    ApplyText();
                    break;
                case 2:
                    selectedLevel++;
                    MoveShip();
                    ApplyText();
                    break;
                case 3:
                    selectedLevel = 1;
                    MoveShip();
                    ApplyText();
                    break;
            }
        }

        if (_input < 0)
        {
            switch (selectedLevel)
            {
                case 1:
                    selectedLevel = 3;
                    MoveShip();
                    ApplyText();
                    break;
                case 2:
                    selectedLevel--;
                    MoveShip();
                    ApplyText();
                    break;
                case 3:
                    selectedLevel--;
                    MoveShip();
                    ApplyText();
                    break;
            }
        }
    }

    void MoveShip()
    {
        shipPos = shipTransform.position;
        shipDest = levelObjects[selectedLevel - 1].transform.position;
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

        yield return null;
    }

    void ApplyText()
    {
        levelSelectedText.text = "Level " + selectedLevel.ToString();
    }
}
