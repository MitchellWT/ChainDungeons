using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 1;
    public Animator enemyAnimator;
    public SpriteRenderer enemySpriteRenderer;
    private void Start()
    {
        enemySpriteRenderer = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
    }

    public void enemyHit()
    {
        enemyHealth--;

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

    private IEnumerator enemyHitFeedback()
    {
        enemyAnimator.SetBool("isHurt", true);

        yield return new WaitForSeconds(0.1f);

        enemyAnimator.SetBool("isHurt", false);
    }

    private IEnumerator enemyDeathFeedback()
    {
        enemyAnimator.SetBool("isDead", true);

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}
