using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScore : MonoBehaviour {

    public static EndGameScore instance;
    [Space][Header("Totals")]
    public int finalScore;
    public int bonusScores;
    #region individual component ints

    [Space][Header("Individual components")][Tooltip("These populate automaticly")]
    [SerializeField]
    int bottles = 0;
    [SerializeField]
    int florpsFilled = 0;
    [SerializeField]
    int firesPutOut = 0;
    [SerializeField]
    int firesActive = 0;
    [SerializeField]
    int florpsBurned = 0;
    [SerializeField]
    int pipesFixed = 0;
    #endregion
    #region Add score func
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void AddFlorpScore(int score)
    {
        florpsFilled += score;
    }
    public void AddBottleScore(int score)
    {
        bottles += score;
       
    }
    public void AddInsertedFlorp(int score)
    {
        florpsBurned += score;
    }
    public void FixedPipes(int score)
    {
        pipesFixed += score;
    } 
    public void FirePutOut(int score)
    {
        firesPutOut += score;
    }
    public void FiresActive(int score)
    {
        firesActive += score;
    }

    #endregion
    public void giveFinalScore()
    {
        finalScore = (int)ShipHealth.instance.shipCurrenHealth + 1;
    }
    private void Update()
    {
        bonusScores = (florpsFilled + bottles + firesPutOut);
    }
}
