using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    public Animator[] animators = new Animator[3];
    [Space]
    [SerializeField] float initialDelay = 0.75f;
    [SerializeField] float animationDelay = 1.0f;

    [SerializeField] string tutorialSceneName = "Ship_Level_Tutorial NEW";

    void Awake()
    {
        StartCoroutine(FillCogs());
    }

    public IEnumerator FillCogs()
    {
        switch (StarsToAward())
        {
            case 1:
                yield return new WaitForSecondsRealtime(initialDelay); // This is just to make it look better and give players time to see what's going on.
                animators[0].SetBool("Fill_Cog1", true);
                break;
            case 2:
                yield return new WaitForSecondsRealtime(initialDelay); // This is just to make it look better and give players time to see what's going on.
                animators[0].SetBool("Fill_Cog1", true);
                yield return new WaitForSecondsRealtime(animationDelay);
                animators[1].SetBool("Fill_Cog2", true);
                break;
            case 3:
                yield return new WaitForSecondsRealtime(initialDelay); // This is just to make it look better and give players time to see what's going on.
                animators[0].SetBool("Fill_Cog1", true);
                yield return new WaitForSecondsRealtime(animationDelay);
                animators[1].SetBool("Fill_Cog2", true);
                yield return new WaitForSecondsRealtime(animationDelay);
                animators[2].SetBool("Fill_Cog3", true);
                break;
            case 0:
                Debug.LogError("A score of zero should not be possible. Please tell Zach to take a look.");
                break;
            default:
                goto case 1;
        }

        yield break;
    }

    // Returns the number of stars to award based on ship health.
    public int StarsToAward()
    {
        // Dirty implementation for tutorial case where there is no GameplayLoopManager.
        if (SceneManager.GetActiveScene().name == tutorialSceneName)
        {
            return 3; // Start the players off on a high note.
        }

        // Completing the level with maximum ship health awards three stars.
        if (GameplayLoopManager.instance.shipCurrenHealth >= GameplayLoopManager.instance.shipMaxHealth)
        {
            return 3;
        }
        // Completing the level with at least half of the ship's health awards two stars.
        else if (GameplayLoopManager.instance.shipCurrenHealth >= GameplayLoopManager.instance.shipMaxHealth / 2)
        {
            return 2;
        }
        // Simply completing the level guarantees one star.
        else if (GameplayLoopManager.instance.shipCurrenHealth > 0)
        {
            return 1;
        }
        // This shouldn't ever happen.
        else
        {
            return 0;
        }
    }
}
