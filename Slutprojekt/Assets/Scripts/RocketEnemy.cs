using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemy : Enemy
{   
    void Update()
    {
        CalculateInputTowardsPlayer();

        if (distanceToPlayer<=10 && followPlayer)
        {
            followPlayer = false;
        }
        if (!followPlayer && distanceToPlayer >15)
        {
            followPlayer = true;
        }
        if (!followPlayer)
        {
            RotateInput();
        }
        Move(input);

        Rotate(player.transform.position);

        if (timeSinceLastFire > timeBetweenFires)
        {
            Fire(0);
            timeSinceLastFire = 0;
        }
        timeSinceLastFire += Time.deltaTime;
    }    
}
