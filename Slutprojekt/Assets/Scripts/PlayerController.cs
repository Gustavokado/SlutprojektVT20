using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField]
    private GameObject crossHair;
    private float crossHairSpeed = 30;
    public GameObject projectilePrefab;
    void Update()
    {
        input.x = Input.GetAxis("LeftStickHorizontal");
        input.y = Input.GetAxis("LeftStickVertical")*-1;

        print(input.x);
        print(input.y);

        aim.x = Input.GetAxis("RightStickHorizontal");
        aim.y = Input.GetAxis("RightStickVertical")*-1;



        Move(input);
        crossHair.transform.Translate(input * Time.deltaTime * speed);

        Vector3 newCrossHairPosition = crossHair.transform.position + aim * Time.deltaTime * crossHairSpeed;        
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(newCrossHairPosition);

        if (screenPoint.x < 0 || screenPoint.x > 1 )
        {
            newCrossHairPosition.x = crossHair.transform.position.x;
        }
        if (screenPoint.y < 0 || screenPoint.y > 1)
        {
            newCrossHairPosition.y = crossHair.transform.position.y;
        }

        crossHair.transform.position=newCrossHairPosition;

        Rotate(crossHair.transform.position);

        if (Input.GetButtonDown("RightBumper"))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}
