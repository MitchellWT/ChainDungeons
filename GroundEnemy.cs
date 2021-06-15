using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script ended up being used for the flying
 * skull enemies. There are two separate scripts as
 * the ground enemies were meant to move as well.
 */
public class GroundEnemy : MonoBehaviour
{
    public Rigidbody2D enemyBody;
    /* The start position and end position
     * dictate the bounds of the flying enemy movement.
     */
    public Transform startPosition;
    public Transform endPosition;
    public float movementSpeedAndDirection;
    public bool moveVertical = false;

    // Sets enemy position to start position.
    void Start()
    {
        transform.position = startPosition.position;
    }

    /* Moves enemy along path between start and end position
     * and changes direction If position equals end position.
     * This works move effectively than the method in the 'FlyingEnemy'
     * script.
     */
    void FixedUpdate()
    {
        if (moveVertical)
        {
            enemyBody.velocity = new Vector2(0, movementSpeedAndDirection * Time.fixedDeltaTime);
        }
        else
        {
            enemyBody.velocity = new Vector2(movementSpeedAndDirection * Time.fixedDeltaTime, 0);
        }

        if (((int) transform.position.x == (int) endPosition.position.x) && ((int) transform.position.y == (int) endPosition.position.y))
        {
            Transform tempPosition = endPosition;

            endPosition = startPosition;
            startPosition = tempPosition;
            movementSpeedAndDirection *= -1;
        }
    }
}
