using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector2 input;
    
    public float speed;
    float currentDirection;
    float newDirection;
    Rigidbody2D rb;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    
    protected void Move(Vector2 movement)
    {
        //transform.Translate(movement*Time.deltaTime * speed,Space.World);   
        rb.velocity = movement * speed;
    }

    protected void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    protected void Rotate(Vector2 target)
    {       
        newDirection = -Mathf.Atan(-(target.x-transform.position.x)/-(target.y-transform.position.y))*Mathf.Rad2Deg;
        if (target.y < transform.position.y)
        {
            newDirection += 180;            
        }
        
        transform.Rotate(0,0, newDirection - currentDirection, Space.Self);
        currentDirection = newDirection;

    }
}
