﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : Projectile
{
    [SerializeField]
    int maxLifeTimeIncreaseTimes;
    
    void Update()
    {
        IncreaseLifetime();       
        Move();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Terrain")
        {                       
            if (maxLifeTimeIncreaseTimes>0)
            {
                maxLifetime += .5f;
                maxLifeTimeIncreaseTimes -= 1;
            }
           
            Vector2 normal = collision.GetContact(0).normal;
            float normalAngle = Vector2.SignedAngle(Vector2.up, normal);
            if (normal.x<0)
            {
                normalAngle += 360;
            }
            direction = direction + 2 * (normalAngle - direction);
        }
        else
        {
            base.OnCollisionEnter2D(collision);
        }       
    }
}