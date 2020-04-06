using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Character host;
    public GameObject bar;
    float maxHealth;
    void Start()
    {
        maxHealth = host.GetHealth();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = host.transform.position;
        newPosition.y -= 1;
        transform.position = newPosition;

        float health = host.GetHealth();
        Vector3 newBarScale = bar.transform.localScale;
        newBarScale.x = health / maxHealth;
        bar.transform.localScale = newBarScale;
    }
}
