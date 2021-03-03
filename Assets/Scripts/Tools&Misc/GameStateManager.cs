using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Paused,
    Playing,
    LostByDamage,
    LostByFlorp,
    Won
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public GameState gameState = GameState.Playing;

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
        DontDestroyOnLoad(this.gameObject);
    }

    // Sets the gameState to the given state, such as Paused or either of the lose conditions (for telling players why they lost).
    public void SetGameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            case GameState.LostByDamage:
            case GameState.LostByFlorp:
            case GameState.Won:
                TeleportShader[] players = FindObjectsOfType<TeleportShader>();
                foreach (TeleportShader tele in players)
                {
                    tele.TeleportEffect();
                }
                break;
        }
    }

    // Returns the current gameState, call this in mechanic-related scripts to determine whether or not mechanics should run
    // ex: if (GameStateManager.instance.GetState() != GameState.Paused) { <fire lasers, spread fire, cool engine, accept player input, etc> };
    public GameState GetState()
    {
        return gameState;
    }
}
