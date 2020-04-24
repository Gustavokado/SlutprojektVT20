using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField]
    private GameObject crossHair; //referens till spelarens crosshair och några variabler som behövs för den
    private float crossHairSpeed = 30;
    Vector3 newCrossHairPosition;
   
    private Vector3 aim; //typ input vectorn för crosshairen om man använder controller
    bool mouseControl=true;
    void Update()
    {       
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical"); //tar spelarens input, funkar både för wasd och för controllerns left stick

        aim.x = Input.GetAxis("RightStickHorizontal"); //man siktar med right stick
        aim.y = Input.GetAxis("RightStickVertical")*-1;

        if (Input.GetKeyDown("tab")) //om trycker tab byter man mellan att styra med controllern och musen
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
        //crosshairen flyttas olika beroende på hur man styr
        if (!mouseControl) 
        {
            crossHair.transform.Translate(input * Time.deltaTime * speed); //om man styr med controllern måste den flyttas två gånger, först måste den flyttas likadant som spelaren så den har samma relativa position

            newCrossHairPosition = crossHair.transform.position + aim * Time.deltaTime * crossHairSpeed; //sen flyttas den beroende på aim vectorn
            
            //det under är till för att crosshairen inte ska kunna flyttas utanför skärmen, hittade den här lösningen nånstans
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
            newCrossHairPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //om man styr med musen flyttas den bara till musens position
            newCrossHairPosition.z = 0;
            
        }
        crossHair.transform.position = newCrossHairPosition;


        Rotate(crossHair.transform.position); //roterar spelaren mot crosshairen

        if (Input.GetButtonDown("RightBumper")|| Input.GetButtonDown("Fire1")) //skjuter en projectile när man trycker vänsterklick eller right bumper på controllern
        {
            Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation); 
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>()); //ser till att man inte kolliderar med den projectilen man skjuter
        }
    }
}
