using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector3 input;
    public float speed;
    public float currentRotation = 0f;
    public float newRotation = 0f;
    /*Vector3 currentEulerAngles;
    Quaternion currentRotation;*/

    void Start()
    {
        
    }
    
    
    protected void Move(Vector3 movement)
    {
        transform.Translate(movement*Time.deltaTime * speed);
        newRotation = Mathf.Asin(movement.x / movement.y)*Mathf.Rad2Deg - currentRotation;
        transform.Rotate(0f, 0f, newRotation, Space.World);
        currentRotation = newRotation;
        /*currentEulerAngles = new Vector3(0, 0, Mathf.Sin(movement.x / movement.y) * Mathf.Rad2Deg);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;*/
    }

    void Rotate(float angle)
    {
        /*currentEulerAngles += new Vector3(0, 0, Mathf.Asin(input.x / input.y) * Mathf.Rad2Deg);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;*/
    }

}
