using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : Enemy
{
    [SerializeField]
    float fireAngle; //en till variabel behövs för att ninjan ska skjuta sina projectiles på en vinkel som man kan besämma själv
    void Update() //exakt samma funktionalitet som rocket enemy
    {
        CalculateInputTowardsPlayer(); 
        if (distanceToPlayer <= 10 && followPlayer)
        {
            followPlayer = false;
        }
        if (!followPlayer && distanceToPlayer > 15)
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
            Fire(fireAngle); //då ninjan ska skjuta två projectiles åt olika håll kallas fire metoden två gånger, en gång med positiv fireangle och en gång med negativ
            Fire(-fireAngle);
            timeSinceLastFire = 0;
        }
        timeSinceLastFire += Time.deltaTime;
    }
}
