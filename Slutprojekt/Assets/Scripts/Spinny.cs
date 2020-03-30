using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinny : Projectile
{
    [SerializeField]
    float maxLifeTimeDecay;
    void Start()
    {
        
    }
    
    void Update()
    {
        ReduceLifetime();
        Move();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Terrain")
        {
            
            maxLifetime += maxLifeTimeDecay;
            if (maxLifeTimeDecay>0)
            {
                maxLifeTimeDecay -= .5f;
            }
            print(lifeTime);
            print(maxLifetime);
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
