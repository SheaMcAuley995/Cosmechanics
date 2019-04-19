using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverAnimation : MonoBehaviour
{
    public Animator animator;


    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void FlashButton()
    {
        animator.SetBool("isSelecting", true);
    }
}
