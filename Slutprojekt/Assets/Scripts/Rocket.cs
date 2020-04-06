using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField]
    GameObject explosionPrefab;
    private void Start()
    {
       
    }
    private void Update()
    {
        ReduceLifetime();
        Move();
    }

    protected override void Move()
    {
        speed*=1.1f;
        base.Move();
    }

    protected override void ReduceLifetime()
    {
        base.ReduceLifetime();
        if (lifeTime >= maxLifetime)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
       

        if (collision.gameObject.tag != "Projectile")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);             
        }
       
        base.OnCollisionEnter2D(collision);
    }
}
