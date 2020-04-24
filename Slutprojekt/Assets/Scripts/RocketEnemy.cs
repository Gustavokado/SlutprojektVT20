using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemy : Enemy
{   
    void Update()
    {
        CalculateInputTowardsPlayer(); //inputen måste updateras varje frame

        if (distanceToPlayer<=10 && followPlayer) //ifall enemyn kommer inom ett visst avstånd till spelaren går den över till att cirkla spelaren
        {
            followPlayer = false;
        }
        if (!followPlayer && distanceToPlayer >15) //ifall den sen kommer utanför ett längre avstånd går den tillbaka till att följa efter
        {
            followPlayer = true;
        }
        if (!followPlayer)
        {
            RotateInput(); //ifall den ska cirkla roteras inputen 90 grader
        }
        Move(input);

        Rotate(player.transform.position); //roterar spriten och riktningen som projectiles flyger till att vara mot spelaren

        if (timeSinceLastFire > timeBetweenFires) //om en viss tid har gått ska en projectile skjutas
        {
            Fire(0); //enemyn som skjuter en raket ska bara skjuta den rakt fram (se enemy scriptet)
            timeSinceLastFire = 0;
        }
        timeSinceLastFire += Time.deltaTime;
    }    
}
