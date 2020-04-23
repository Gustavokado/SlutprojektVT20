using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{    
    [SerializeField]
    float damage;
    [SerializeField]
    float activeTime; //den tiden efter den skapas som den gör skada    
    float lifeTime;
    float transparencySpeed = .01f; //används för att göra den genomskinlig
    
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime>=activeTime)
        {
            GetComponent<Collider2D>().enabled = false; //när explosionen inte är aktiv längre stängs collidern av och den börjar bli genomskinlig

            Color color = GetComponent<SpriteRenderer>().color;

            color.a -= transparencySpeed; //hittade den här lösningen nånstans
            if (color.a<=0)
            {
                Destroy(this.gameObject);
            }
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //när något går in i explosionen (eller är där när den skapas) tar det damage
    {
        Character hitCharacter = collision.GetComponent<Character>();
        if (hitCharacter!=null)
        {
            hitCharacter.ReduceHealth(damage);
        }       
    }
}
