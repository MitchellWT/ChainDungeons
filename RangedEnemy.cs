using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform projectilePoint;
    public GameObject projectilePrefab;
    public int projectileInterval = 50;
    private int counter;

    // Start is called before the first frame update
    private void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    // Frame independent update
    private void FixedUpdate()
    {
        counter++;

        if (counter == projectileInterval)
        {
            Shoot();
            counter = 0;
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, projectilePoint.position, projectilePoint.rotation);
    }
}
