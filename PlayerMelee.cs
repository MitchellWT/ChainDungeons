using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

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
    private IEnumerator attackAnimation()
    {
        SpriteRenderer attackSprite = GameObject.FindWithTag("slash").GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        attackSprite.enabled = true;

        yield return new WaitForSeconds(0.1f);

        attackSprite.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
