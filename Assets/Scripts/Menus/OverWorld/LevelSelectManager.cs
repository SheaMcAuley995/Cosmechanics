using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    ////fuck you vv
    //public PlayerController[] players;
    ////fuck you ^^
    //private void Awake()
    //{
    //    players = FindObjectsOfType<PlayerController>();
    //    //I hate this ^^
    //}

    // TODO: Store user data on levels they've beaten & lock levels 2 and 3 until reached
    public void LaunchLevel(string scene)
    {
        //And this vv
        // Very constructive feedback, thanks. Why? What would you do instead?
        switch (OverworldManager.instance.level)
        {
            case OverworldManager.Level.Level1:
                //scene = "TutorialLevel";
                scene = "ZachShipTest";
                break;
            case OverworldManager.Level.Level2:
                scene = "Ship_Level_1";
                break;
            case OverworldManager.Level.Level3:
                scene = "BetaMichaelTest";
                break;
        }

        SceneFader.instance.FadeTo(scene);
    }
}