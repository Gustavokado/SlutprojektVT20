using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float direction;
    public float maxLifetime;
    protected float lifeTime =0;
    bool friendly;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.eulerAngles.z+90;
    }

    protected void ReduceLifetime()
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
        Destroy(gameObject);
        if (friendly)
        {
            //gör damage om träffar enemy
        }
    }
}
