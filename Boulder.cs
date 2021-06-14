using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public LayerMask playerMask;
    public Rigidbody2D boulderBody;
    public Animator boulderAnimator;
    public float horizontalVelocity = -1f;
    public float verticalVelocity = -9.81f;
    private bool playerDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D playerRaycast = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 35f, playerMask);

        if (playerRaycast.collider != null)
        {
            FindObjectOfType<AudioManager>().PlayClip("boulder-chain-cut");

            boulderBody.gravityScale = 1;
            boulderBody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            StartCoroutine(DissolveBoulder());
        }
    }

    IEnumerator DissolveBoulder()
    {
        yield return new WaitForSeconds(2.5f);

        boulderAnimator.SetBool("isDead", true);

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            FindObjectOfType<AudioManager>().PlayClip("boulder-hit-ground");
        }
    }
}
