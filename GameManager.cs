using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Manages game scenes for when the player
 * dies or the lever is activated.
 */
public class GameManager : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionTime = 1f;
    public Text playerLivesText;
    public int currentScene;

    /* Sets the player lives in the HUD. 
     * This occurs when all scenes start.
     */
    private void Start()
    {
        playerLivesText.text = "x " + GlobalVariables.playerLives.ToString();

    }

    // Unused update method.
    private void Update()
    {
        
    }

    /* Called when the player dies. Freezes the player object,
     * removes a life, and checks If the player has ran out of
     * lives. If the player has ran out of lives the game over
     * scene is shown, other wise the current scene resets.
     */
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

    /* The below two change scene methods stop all
     * coroutines and starts their respective coroutine.
     * Their is a distinct difference in the transitions of
     * each coroutine.
     */
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

    /* Triggers scene change animation and then load the scene.
     * This animation includes sound clips for world building.
     */
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

    /* Triggers scene change animation and then load the scene.
     * This animation just fades to black.
     */
    private IEnumerator changeSceneTransition(int scene)
    {
        transitionAnimator.SetTrigger("StartFade");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(scene);
    }
}
