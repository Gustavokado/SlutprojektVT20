using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //en del simpla variabler som alla projectiles behöver och som används i metoder i den här klassen
    [SerializeField] 
    protected float speed;
    protected float direction;
    [SerializeField]
    protected float maxLifetime;
    protected float lifeTime =0;
    [SerializeField]
    protected float damage;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //skapar en referens till dess rigidbody 
        direction = transform.eulerAngles.z+90; //fixar dess direction, av någon anledning åker den sidledes annars
    }

    protected virtual void IncreaseLifetime()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifetime)
        {
            Destroy(this.gameObject); //projectilen förstörs när den existerat tillräckligt länge
        }
    }
  
    protected virtual void Move()
    {     
        rb.velocity = new Vector2(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad)) * speed; //flyttar projectilen i dess direction med dess speed      
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) //kallas när projectilen kolliderar med något
    {
        if (collision.gameObject.tag == "Projectile") //om den kolliderar med en annan projectile ignoreras kollisionen
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

        else
        {
            Destroy(this.gameObject); //om den träffar något annat (terräng/annan karaktär) förstörs den
        }

        Character hitCharacter = collision.gameObject.GetComponent<Character>();
        if (hitCharacter!=null) //om den träffar ett objekt som har ett character script kallas dess reducehealth metod med projectilens damage variabel 
        {
            hitCharacter.ReduceHealth(damage);
        }       
    }
}
