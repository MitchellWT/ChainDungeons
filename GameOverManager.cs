using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    /* Checks If the play has pressed 'K' or 'L' and
     * respectively restart or quits the game.
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GlobalVariables.playerLives = 3;
            SceneManager.LoadScene(0);
        }   
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Application.Quit();
        } 
    }
}
