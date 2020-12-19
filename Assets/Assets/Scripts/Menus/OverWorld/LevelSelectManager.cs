using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;


public class LevelSelectManager : MonoBehaviour
{
    int playerId = 0;
    Player[] players = new Player[4];
    [SerializeField] string sceneName;


    void Start()
    {
        players[0] = ReInput.players.GetPlayer(playerId);
        players[1] = ReInput.players.GetPlayer(playerId + 1);
        players[2] = ReInput.players.GetPlayer(playerId + 2);
        players[3] = ReInput.players.GetPlayer(playerId + 3);
    }

    void Update()
    {
        foreach (Player player in players)
        {
            if (player.GetButtonDown("Sprint"))
            {
                GoToPreviousScene(sceneName);
            }
        }
    }

    public void GoToPreviousScene(string scene)
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            Destroy(player.gameObject);
        }

        SceneFader.instance.FadeTo(scene);
    }
}