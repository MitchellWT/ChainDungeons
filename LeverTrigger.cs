using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    // Dialogue text that is played at the end of the level.
    [TextArea(3, 10)]
    public string dialogueText;

    /* The below options dictate which level the key selection
     * will go to. The rewards denote how many live the player gets
     * If they select that option.
     */
    public int optionOne;
    public int optionTwo;
    public int optionOneReward = 0;
    public int optionTwoReward = 0;

    public void selectOptionOne()
    {
        GlobalVariables.playerLives += optionOneReward;
        FindObjectOfType<GameManager>().leverChangeScene(optionOne);
    }

    public void selectOptionTwo()
    {
        GlobalVariables.playerLives += optionTwoReward;
        FindObjectOfType<GameManager>().leverChangeScene(optionTwo);
    }
}
