using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script was not used in the game
 * and only exists for testing purposes.
 */
public class FlyingEnemy : MonoBehaviour
{
    public Rigidbody2D enemyBody;
    /* The start position and end position
     * dictate the bounds of the flying enemy movement.
     */
    public Transform startPosition;
    public Transform endPosition;
    public float movementSpeedAndDirection;

    // Sets enemy position to start position.
    void Start()
    {
        transform.position = startPosition.position;
    }

    /* Moves enemy along path between start and end position
     * and changes direction If position equals end position.
     */
    void FixedUpdate()
    {
        enemyBody.velocity = new Vector2(movementSpeedAndDirection * Time.fixedDeltaTime, 0);

        if (transform.position == endPosition.position)
        {
            transform.position = startPosition.position;
        }
    }
}
