using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform projectilePoint;
    public GameObject projectilePrefab;
    /* The below two variables are used to
     * determine that projectile interval.
     * This could be implemented more elegantly.
     */
    public int projectileInterval = 50;
    private int counter;

    private void Start()
    {
        counter = 0;
    }

    // Unused Update method.
    private void Update()
    {
        
    }

    /* Increment counter by one and check If the
     * counter is equal to the projectile interval
     * variable. If so the 'Shoot' method is called, 
     * and the counter is reset.
     */
    private void FixedUpdate()
    {
        counter++;

        if (counter == projectileInterval)
        {
            Shoot();
            counter = 0;
        }
    }

    // Create projectile at projectile point.
    private void Shoot()
    {
        Instantiate(projectilePrefab, projectilePoint.position, projectilePoint.rotation);
    }
}
