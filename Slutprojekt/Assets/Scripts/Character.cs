using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //mycket av variablerna och metoderna är protected då subklasserna använder mycket av samma saker
    protected Vector2 input; //riktningen som karaktären ska röra sig
    [SerializeField]
    protected float speed;
    private float currentDirection;
    private float newDirection;
    private Rigidbody2D rb;
    [SerializeField]
    private float health;
    [SerializeField]
    HealthBar healthBar;
    [SerializeField]
    protected Projectile projectilePrefab;

    protected virtual void Start() //defenierar en del saker som behövs som rigidbody och dess healthbar
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(healthBar, transform.position, transform.rotation);
        healthBar.SetHost(this);
    }
       
    protected void Move(Vector2 input) //kallas från subklassernas update
    {       
        rb.velocity = input * speed;
    }

    protected void Rotate(Vector2 target) //roterar karaktären mot en viss punkt (crosshairen/spelaren)
    {       
        newDirection = -Mathf.Atan(-(target.x-transform.position.x)/-(target.y-transform.position.y))*Mathf.Rad2Deg; //fancy matte som jag hittade/testade mig fram
        if (target.y < transform.position.y)
        {
            newDirection += 180;            
        }
        
        transform.Rotate(0,0, newDirection - currentDirection, Space.Self);
        currentDirection = newDirection;
    }

    public void ReduceHealth(float damage) //kallas när någon projectile träffar en karaktär och tar in dess damage variabel som parameter
    {
        health -= damage;
        if (health<=0)
        {
            Destroy(healthBar.gameObject);
            Destroy(this.gameObject);           
        }
    }

    public float GetHealth() //används av healthbar scriptet för att health inte ska behöva vara public
    {
        return health;
    }
}
