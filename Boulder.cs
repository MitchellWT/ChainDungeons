using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    /* This layer mask contains a layer
     * that only consists of the player
     * object.
     */
    public LayerMask playerMask;
    public Rigidbody2D boulderBody;
    public Animator boulderAnimator;
    /* The two below velocities are used
     * for the boulder movement.
     */
    public float horizontalVelocity = -1f;
    public float verticalVelocity = -9.81f;
    private bool playerDetected = false;

    // Unused start, unnecessary code.
    void Start()
    {
        
    }

    /* In the update method, a check is performed to see If the
     * player is under the boulder. This check uses raycasting.
     * If the player is under the boulder the boulder will fall.
     */
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

    /* Removes the boulder game object after
     * a period of time (2.6 seconds).
     */
    IEnumerator DissolveBoulder()
    {
        yield return new WaitForSeconds(2.5f);

        boulderAnimator.SetBool("isDead", true);

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }

    /* Plays audio clip when the boulder hits the
     * ground.
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            FindObjectOfType<AudioManager>().PlayClip("boulder-hit-ground");
        }
    }
}
