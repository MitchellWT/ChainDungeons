using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public Text playerLivesText;
    public int currentScene;

    private void Start()
    {
        playerLivesText.text = "x " + GlobalVariables.playerLives.ToString();

    }
    private void Update()
    {
        
    }

    public void playerDied()
    {
        Rigidbody2D playerBody = GameObject.FindGameObjectsWithTag("player")[0].GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        CapsuleCollider2D playerCollider = GameObject.FindGameObjectsWithTag("player")[0].GetComponent(typeof(CapsuleCollider2D)) as CapsuleCollider2D;
        playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
        playerCollider.enabled = false;

        GlobalVariables.playerLives--;

        if (GlobalVariables.playerLives > 0)
        {
            StopAllCoroutines();
            StartCoroutine(changeSceneTransition(currentScene));
        }
        else
        {
            Destroy(FindObjectOfType<AudioManager>().gameObject);
            // Game over scene
            StopAllCoroutines();
            StartCoroutine(changeSceneTransition(8));
        }
    }

    public void changeScene(int scene)
    {
        StopAllCoroutines();
        StartCoroutine(changeSceneTransition(scene));
    }

    public void leverChangeScene(int scene)
    {
        StopAllCoroutines();
        StartCoroutine(changeSceneLeverTransition(scene));
    }

    private IEnumerator changeSceneLeverTransition(int scene)
    {
        transitionAnimator.SetTrigger("StartFade");

        yield return new WaitForSeconds(transitionTime);

        FindObjectOfType<AudioManager>().StopMusic();
        FindObjectOfType<AudioManager>().PlayClip("lever-pull");

        yield return new WaitForSeconds(2);

        FindObjectOfType<AudioManager>().PlayClip("bone-gear");

        yield return new WaitForSeconds(4);

        SceneManager.LoadScene(scene);
    }
    private IEnumerator changeSceneTransition(int scene)
    {
        transitionAnimator.SetTrigger("StartFade");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(scene);
    }
}
