using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Paused,
    Running
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameState gameState = GameState.Running;

    void Awake()
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
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public GameState GetState()
    {
        return gameState;
    }
}
