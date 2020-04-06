using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField]
    private GameObject crossHair;
    private float crossHairSpeed = 30;
    Vector3 newCrossHairPosition;
    public GameObject projectilePrefab;
    private Vector3 aim;
    bool mouseControl=true;
    void Update()
    {
        print(health);
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        //print(input.x); print(input.y);

        aim.x = Input.GetAxis("RightStickHorizontal");
        aim.y = Input.GetAxis("RightStickVertical")*-1;

        if (Input.GetKeyDown("tab"))
        {
            if (mouseControl)
            {
                mouseControl = false;
            }
            else
            {
                mouseControl = true;
            }
        }

        Move(input);
        if (!mouseControl)
        {
            crossHair.transform.Translate(input * Time.deltaTime * speed);

            newCrossHairPosition = crossHair.transform.position + aim * Time.deltaTime * crossHairSpeed;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(newCrossHairPosition);

            if (screenPoint.x < 0 || screenPoint.x > 1)
            {
                newCrossHairPosition.x = crossHair.transform.position.x;
            }
            if (screenPoint.y < 0 || screenPoint.y > 1)
            {
                newCrossHairPosition.y = crossHair.transform.position.y;
            }
            
        }
        else
        {
            newCrossHairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newCrossHairPosition.z = 0;
            
        }
        crossHair.transform.position = newCrossHairPosition;


        Rotate(crossHair.transform.position);

        if (Input.GetButtonDown("RightBumper")|| Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
