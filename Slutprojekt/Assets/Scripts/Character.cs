using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector2 input;
    protected Vector3 aim;
    public float speed;
    float currentDirection;
    float newDirection;
  
    void Start()
    {
        
    }
    
    
    protected void Move(Vector2 movement)
    {
        transform.Translate(movement*Time.deltaTime * speed,Space.World);
        
        /*currentEulerAngles = new Vector3(0, 0, Mathf.Sin(movement.x / movement.y) * Mathf.Rad2Deg);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;*/
    }

    protected void Rotate(Vector2 target)
    {       
        newDirection = -Mathf.Atan(-(target.x-transform.position.x)/-(target.y-transform.position.y))*Mathf.Rad2Deg;
        if (target.y < transform.position.y)
        {
            newDirection += 180;            
        }
        
        transform.Rotate(0,0, newDirection - currentDirection, Space.World);
        currentDirection = newDirection;

    }
}
