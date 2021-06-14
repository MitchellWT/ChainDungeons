using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayClip("dungeon-ambient");
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<AudioManager>().CheckIfPlaying("player-footsteps"))
        {
            FindObjectOfType<AudioManager>().PauseClip("player-footsteps");
        }

        if (FindObjectOfType<AudioManager>().CheckIfMusicPlaying())
        {
            FindObjectOfType<AudioManager>().StopMusic();
        }

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
