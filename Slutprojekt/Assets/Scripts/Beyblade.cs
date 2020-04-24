using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : Projectile
{
    [SerializeField]
    int maxLifeTimeIncreaseTimes; //varje gång den studsar ökar dess max life time
    
    void Update()
    {
        IncreaseLifetime(); //använder basklassens metoder för att öka sin livstid och flytta på sig       
        Move();
    }

    protected override void OnCollisionEnter2D(Collision2D collision) //behöver mer funktionalitet än basversionen
    {
        if (collision.gameObject.tag=="Terrain") //om den träffar terräng ska den studsa
        {                       
            if (maxLifeTimeIncreaseTimes>0) //dens max lifetime ökar när den träffar terräng fast bara ett visst antal gånger
            {
                maxLifetime += .5f;
                maxLifeTimeIncreaseTimes -= 1;
            }
           
            Vector2 normal = collision.GetContact(0).normal; //får normalen av ytan den träffar
            float normalAngle = Vector2.SignedAngle(Vector2.up, normal); //får vinkeln av normalen
            if (normal.x<0)
            {
                normalAngle += 360;
            }
            direction = direction + 2 * (normalAngle - direction); //räknar ut vad den nya direction borde vara för att speglas mot normalen
        }
        else //om den träffar något som inte är terräng körs basversionen
        {
            base.OnCollisionEnter2D(collision);
        }       
    }
}
