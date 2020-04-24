using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField]
    GameObject explosionPrefab;
  
    private void Update() //samma som beybladen
    {
        IncreaseLifetime();
        Move();
    }

    protected override void Move()
    {
        speed*=1.1f; //raketen ökar hastighet varje frame innan den rör på sig genom basmetoden
        base.Move();
    }

    protected override void IncreaseLifetime() //samma funktionalitet som basmetoden plus att den skapar en explosion om den dör i luften
    {
        base.IncreaseLifetime();
        if (lifeTime >= maxLifetime) 
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }        
    }

    protected override void OnCollisionEnter2D(Collision2D collision) //samma funktionalitet som basmetoden plus att den skapar en explosion om den träffar något som inte är en projectile
    {
        if (collision.gameObject.tag != "Projectile")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);             
        }       
        base.OnCollisionEnter2D(collision);
    }
}
