using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public Rigidbody2D projectileBody;


    // Start is called before the first frame update
    private void Start()
    {
        projectileBody.velocity = -transform.right * projectileSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

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
