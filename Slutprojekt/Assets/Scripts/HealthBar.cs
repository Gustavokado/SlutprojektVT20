using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{    
    Character host; //den karaktären som har den här healthbaren
    [SerializeField]
    GameObject bar; //ett annat objekt som är delen som faktiskt går ner när hosten tar damage
    float maxHealth;
    void Start()
    {
        maxHealth = host.GetHealth(); //när healthbaren skapas av en karaktär
    }
    
    void LateUpdate() //lateupdate för att den ska flyttas efter att karaktären har flyttats, då karaktären använder rigidbodys och krafter för att flytta och den här använder transform.translate hackar healthbaren efter annars
    {
        Vector3 newPosition = host.transform.position;
        newPosition.y -= 1;
        transform.position = newPosition; //flyttar health baren till att vara en enhet under dess host

        float health = host.GetHealth();
        Vector3 newBarScale = bar.transform.localScale;
        newBarScale.x = health / maxHealth;
        bar.transform.localScale = newBarScale; //ändrar barens x-scale för att visa hur mycket som är kvar, pivot pointen är längst till vänster så när man skalar den ser det ut som att den sjunker åt vänster
    }

    public void SetHost(Character character) //kallas när en health bar skapas av en character
    {
        host = character;
    }
}
