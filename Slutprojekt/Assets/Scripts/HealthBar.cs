using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{    
    Character host; //den karaktären som har den här healthbaren
    [SerializeField]
    GameObject bar; //ett annat objekt som är delen som faktiskt går ner
    float maxHealth;
    void Start()
    {
        maxHealth = host.GetHealth();
    }
    
    void LateUpdate()
    {
        Vector3 newPosition = host.transform.position;
        newPosition.y -= 1;
        transform.position = newPosition; //flyttar health baren till att vara en enhet under dess host

        float health = host.GetHealth();
        Vector3 newBarScale = bar.transform.localScale;
        newBarScale.x = health / maxHealth;
        bar.transform.localScale = newBarScale; //ändrar barens x-scale för att visa hur mycket som är kvar, pivot pointen är längst till vänster
    }

    public void SetHost(Character character)
    {
        host = character;
    }
}
