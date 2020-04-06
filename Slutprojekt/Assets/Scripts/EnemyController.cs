using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    public GameObject projectilePrefab;
    protected GameObject player;
    protected float distanceToPlayer;
    protected float timeSinceLastFire;
    public float timeBetweenFires;
    protected bool followPlayer = true;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    

    protected void Fire(float direction)
    {
        Quaternion newRotation = transform.rotation;
        Vector3 currentAngles = transform.rotation.eulerAngles;
        currentAngles.z += direction;
        newRotation.eulerAngles = currentAngles;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, newRotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    protected void CalculateInputTowardsPlayer()
    {
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        if (x > 0)
        {
            x = Mathf.Pow(x, 2);
        }
        else
        {
            x = Mathf.Pow(x, 2) * -1;
        }
        if (y > 0)
        {
            y = Mathf.Pow(y, 2);
        }
        else
        {
            y = Mathf.Pow(y, 2) * -1;
        }

        distanceToPlayer = Mathf.Sqrt(Mathf.Abs(x) + Mathf.Abs(y));

        input.x = x / (Mathf.Abs(x) + Mathf.Abs(y));
        input.y = y / (Mathf.Abs(x) + Mathf.Abs(y));
    }

    protected void RotateInput()
    {
        float x = input.x;
        float y = input.y;
        
        input.x = y;
        input.y = x * -1;
    }
}
