using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemy : EnemyController
{
    bool followPlayer = true;
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

    void RotateInput()
    {
        float x = input.x;
        float y = input.y;
        /*if (x>0 && y>0)
        {            
            input.x = y;
            input.y = -x;
        }
        else if (x>0 && y<0)
        {           
            input.x = y;
            input.y = -x;
        }
        else if (x < 0 && y < 0)
        {
            input.x = y;
            input.y = -x;
        }
        else if (x < 0 && y > 0)
        {
            input.x = y;
            input.y = -x;
        }*/
        input.x = y;
        input.y = x * -1;
       
    }
}
