using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the plays melee interactions.
public class PlayerMelee : MonoBehaviour
{
    /* The transform for the separate
     * attack point game object.
     */
    public Transform attackPoint;
    public float attackRange = 0.5f;
    /* This layer mask contains a layer
     * that only consists of the enemy
     * objects.
     */
    public LayerMask enemyLayer;

    // Unused start method.
    private void Start()
    {
        
    }

    /* Checks If the player has pressed
     * the attack key ('K') and triggers the 
     * 'Attack' method.
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    /* Plays attack animation with sound and checks
     * Multiple enemies can be hit. If an enemy was hit the 'enemyHit'
     * method was called on said enemy.
     */
    private void Attack()
    {
        StopAllCoroutines();
        StartCoroutine(attackAnimation());

        FindObjectOfType<AudioManager>().PlayClip("dagger-slash");

        Collider2D[] attackedEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in attackedEnemies)
        {
            Component enemyComponent = enemy.gameObject.GetComponent(typeof(Enemy));
            ((Enemy) enemyComponent).enemyHit();
        }
    }

    /* Renders attack image for 0.1 seconds 
     * and then unrenders it. This sprite renderer
     * should be stored in the object. Calling it every time
     * the animation occurs is not optimized.
     */
    private IEnumerator attackAnimation()
    {
        SpriteRenderer attackSprite = GameObject.FindWithTag("slash").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        attackSprite.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackSprite.enabled = false;
    }

    // Helper method for viewing the 'attackPoint' area.
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
