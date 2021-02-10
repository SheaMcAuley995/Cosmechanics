using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueTrigger trigger;

    //[Header("Text Objects")]
    //public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    Queue<string> sentences = new Queue<string>();

    [Header("Speed Settings")]
    public float letterPause = 0.1f;
    public float timeBetweenSentences = 3f;

    [Space]
    public Animator animator;
    PlayerController[] playerControllers;



    private void Start()
    {
        playerControllers = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < playerControllers.Length; i++)
        {
            playerControllers[i].dialogue = this;
        }
    }
    // Call this in DialogueTrigger whenever you wish to start the tutorial interaction
    public void StartDialogue(Dialogue dialogue)
    {

        ///This was used in order to swap the player to Dialogue Input when that was implimented #Don't delete#
        //for (int i = 0; i < playerControllers.Length; i++)
        //{
        //    playerControllers[i].myInput -= playerControllers[i].GameplayInput;
        //    playerControllers[i].myInput += playerControllers[i].DialogueInput;
        //}

        animator.SetBool("IsOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // Runs through the next inspector set dialogue
    public void DisplayNextSentence()
    {
        AudioEventManager.instance.PlaySound("Dialogue Trigger");
        if (sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    // Plays one letter at a time for each sentence
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            
            dialogueText.text += letter;

            // Sound manager stuff

            yield return new WaitForSeconds(letterPause);
            
        }

        yield return new WaitForSeconds(timeBetweenSentences);
        DisplayNextSentence();
    }

    // Self explanatory
    public IEnumerator EndDialogue()
    {
        ///This was used in order to swap the player to Dialogue Input when that was implimented #Don't delete#
      // for (int i = 0; i < playerControllers.Length; i++)
      // {
      //     playerControllers[i].myInput += playerControllers[i].GameplayInput;
      //     playerControllers[i].myInput -= playerControllers[i].DialogueInput;
      // }

        

        animator.SetBool("IsOpen", false);
        StopAllCoroutines();
        yield return new WaitForSeconds(.1f);
    }
}
