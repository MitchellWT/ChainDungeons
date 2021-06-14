using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public Rigidbody2D enemyBody;
    public Transform startPosition;
    public Transform endPosition;
    public float movementSpeedAndDirection;
    public bool moveVertical = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition.position;
    }

    // Frame independent update method
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
