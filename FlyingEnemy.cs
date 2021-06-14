using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Rigidbody2D enemyBody;
    public Transform startPosition;
    public Transform endPosition;
    public float movementSpeedAndDirection;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition.position;
    }

    // Frame independent update method
    void FixedUpdate()
    {
        enemyBody.velocity = new Vector2(movementSpeedAndDirection * Time.fixedDeltaTime, 0);

        if (transform.position == endPosition.position)
        {
            transform.position = startPosition.position;
        }
    }
}
