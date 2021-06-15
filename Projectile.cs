using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public Rigidbody2D projectileBody;

    /* Sets the velocity of the projectile as 
     * soon as its instantiated.
     */
    private void Start()
    {
        projectileBody.velocity = -transform.right * projectileSpeed;
    }

    // Unused update method.
    private void Update()
    {
        
    }

    /* Checks If the collider was the player and kills
     * the player If so. Also destroys the object unless 
     * it hits the ranged enemy. This check is because the
     * projectile is instantiated inside the ranged object.
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "player")
        {
            FindObjectOfType<GameManager>().playerDied();
        }

        if (collider.gameObject.tag != "ranged-enemy")
        {
            Destroy(gameObject);
        }
    }
}
