using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script can be added to any
 * game object to turn it into an enemy.
 */
public class Enemy : MonoBehaviour
{
    /* Amount of times the player needs to hit
     * the enemy.
     */
    public int enemyHealth = 1;
    public Animator enemyAnimator;
    public SpriteRenderer enemySpriteRenderer;
    
    // Sets enemy sprite renderer.
    private void Start()
    {
        enemySpriteRenderer = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
    }

    /* Registers an enemy hit and checks If
     * the enemy has died. Plays a audio based on
     * the object tag. Also plays a enemy death animation.
     */
    public void enemyHit()
    {
        enemyHealth--;

        /* The below (commented) coroutine were
         * meant to play an animation If the enemy was hit
         * However, this was not fully implemented due to time
         * constraint.
         */
        //StopAllCoroutines();
        //StartCoroutine(enemyHitFeedback());

        if (enemyHealth <= 0)
        {
            if (gameObject.tag == "ground-enemy")
            {
                FindObjectOfType<AudioManager>().PlayClip("melee-death");
            }
            else if (gameObject.tag == "ranged-enemy")
            {
                FindObjectOfType<AudioManager>().PlayClip("ranged-death");
            }

            StopAllCoroutines();
            StartCoroutine(enemyDeathFeedback());
        }
    }

    /* Plays feedback animation for hitting an
     * enemy.
     */
    private IEnumerator enemyHitFeedback()
    {
        enemyAnimator.SetBool("isHurt", true);

        yield return new WaitForSeconds(0.1f);

        enemyAnimator.SetBool("isHurt", false);
    }

    /* Plays feedback animation for killing an
     * enemy.
     */
    private IEnumerator enemyDeathFeedback()
    {
        enemyAnimator.SetBool("isDead", true);

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
