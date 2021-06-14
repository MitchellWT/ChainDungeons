using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string dialogueText;

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
