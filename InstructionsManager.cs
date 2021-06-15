using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    /* Pauses the dungeon-ambient audio when
     * the instructions screen starts. This is used
     * when the player restarts the game, It doesn't have
     * an effect when the game starts initially.
     */    
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayClip("dungeon-ambient");
    }

    // Update is called once per frame.
    void Update()
    {
        /* These checks should not be ran in the update method. Ideally
         * they would be in the start method but for some reason they
         * weren't effecting the audio manager on start. I believe it may have
         * something to do with the singleton pattern and the check calling the
         * wrong instance.
         */
        if (FindObjectOfType<AudioManager>().CheckIfPlaying("player-footsteps"))
        {
            FindObjectOfType<AudioManager>().PauseClip("player-footsteps");
        }

        if (FindObjectOfType<AudioManager>().CheckIfMusicPlaying())
        {
            FindObjectOfType<AudioManager>().StopMusic();
        }

        /* Checks If 'Escape', 'K', or 'L' was pressed. If it
         * was pressed the game starts. The 'else if' statement is
         * unnecessary as the 'Escape' key is used in the first 'if'.
         */
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(1);
        }   
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
    }
}
