using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public Animator[] animators = new Animator[3];
    [Space]
    [SerializeField] float initialDelay = 1.0f;
    [SerializeField] float animationDelay = 1.5f;

    void Awake()
    {
        //StartCoroutine(FillCogs());
    }

    public IEnumerator FillCogs()
    {
        yield return new WaitForSecondsRealtime(initialDelay);

        animators[0].SetBool("Fill_Cog1", true);
        yield return new WaitForSecondsRealtime(animationDelay);

        animators[1].SetBool("Fill_Cog2", true);
        yield return new WaitForSecondsRealtime(animationDelay);

        animators[2].SetBool("Fill_Cog3", true);

        yield break;
    }
}
