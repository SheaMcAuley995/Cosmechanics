using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    //fuck you vv
    public PlayerController[] players;
    //fuck you ^^
    private void Awake()
    {
        players = FindObjectsOfType<PlayerController>();
        //I hate this ^^
    }

    // TODO: Store user data on levels they've beaten & lock levels 2 and 3 until reached
    public void LaunchLevel(string scene)
    {
        //And this vv
        switch (OverworldManager.instance.level)
        {
            case OverworldManager.Level.Level1:
                scene = "CopyOfMainMAp";
                break;
            case OverworldManager.Level.Level2:
                scene = "BetaMichaelTest";
                break;
            case OverworldManager.Level.Level3:
                scene = "NewMichaelTest";
                break;
        }

        SceneFader.instance.FadeTo(scene);
    }
}
//FUUUUUUUUUUUCK