using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    /* Text object in the dialogue
     * box.
     */
    public Text dialogueText;
    public Animator dialogueBoxAnimator;
    private bool isDialogue = false;

    public bool GetIsDialogue()
    {
        return isDialogue;
    }

    /* Displays the dialogue box and 
     * calls display method.
     */
    public void StartDialogue(string dialogue)
    {
        dialogueBoxAnimator.SetBool("isOpen", true);
        isDialogue = true;


        this.DisplayDialogue(dialogue);
    }

    /* Sets dialogue text to zero and 
     * starts coroutine for displaying text.
     */
    public void DisplayDialogue(string dialogue)
    {
        dialogueText.text = "";

        if (dialogue != null)
        {
            StopAllCoroutines();
            StartCoroutine(TypeDialogue(dialogue));            
        }
    }

    /* Displays text by adding a letter each frame.
     * This interval can be set by setting the yield
     * return value.
     */
    private IEnumerator TypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    // Closes the dialogue box.
    public void EndDialogue()
    {
        dialogueBoxAnimator.SetBool("isOpen", false);
        isDialogue = false;
    }
}
