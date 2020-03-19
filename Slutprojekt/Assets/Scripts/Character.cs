using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Vector3 input;
    public float speed;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;

    void Start()
    {
        
    }


    protected void Move(Vector3 movement)
    {
        transform.Translate(input*Time.deltaTime * speed);

        currentEulerAngles = new Vector3(0, 0, Mathf.Asin(input.x / input.y) * Mathf.Rad2Deg);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }

    void Rotate(float angle)
    {
        currentEulerAngles += new Vector3(0, 0, Mathf.Asin(input.x / input.y) * Mathf.Rad2Deg);
        currentRotation.eulerAngles = currentEulerAngles;
        transform.rotation = currentRotation;
    }

}
