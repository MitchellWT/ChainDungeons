using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Animator dialogueBoxAnimator;
    private bool isDialogue = false;

    public bool GetIsDialogue()
    {
        return isDialogue;
    }

    public void StartDialogue(string dialogue)
    {
        dialogueBoxAnimator.SetBool("isOpen", true);
        isDialogue = true;


        this.DisplayDialogue(dialogue);
    }

    public void DisplayDialogue(string dialogue)
    {
        dialogueText.text = "";

        if (dialogue != null)
        {
            StopAllCoroutines();
            StartCoroutine(TypeDialogue(dialogue));            
        }
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("isOpen", false);
        isDialogue = false;
    }
}
