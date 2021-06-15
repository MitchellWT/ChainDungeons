using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    /* Pauses the player footstep audio when
     * the credit screen starts.
     */
    void Start()
    {
        FindObjectOfType<AudioManager>().PauseClip("player-footsteps");
    }

    /* Checks If 'Escape', 'K', or 'L' was pressed. If it
     * was pressed the game quits.
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            Application.Quit();
        }
    }
}
