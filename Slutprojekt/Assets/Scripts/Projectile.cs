using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    protected float direction;
    [SerializeField]
    protected float maxLifetime;
    protected float lifeTime =0;
    [SerializeField]
    protected float damage;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.eulerAngles.z+90;
    }

    protected virtual void IncreaseLifetime()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifetime)
        {
            Destroy(this.gameObject);
        }
    }
  
    protected virtual void Move()
    {     
        rb.velocity = new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad)) * speed;           
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        else
        {
            Destroy(this.gameObject);
        }

        Character hitCharacter = collision.gameObject.GetComponent<Character>();
        if (hitCharacter!=null)
        {
            hitCharacter.ReduceHealth(damage);
        }       
    }
}
