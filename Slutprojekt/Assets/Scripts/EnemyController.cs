﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    public GameObject projectilePrefab;
    GameObject player;
    float timeSinceLastFire;
    public float timeBetweenFires;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        if (x>0)
        {
            x = Mathf.Pow(x, 2);
        }
        else
        {
            x = Mathf.Pow(x, 2)*-1;
        }
        if (y > 0)
        {
            y = Mathf.Pow(y, 2);
        }
        else
        {
            y = Mathf.Pow(y, 2) * -1;
        }

        input.x = x / (Mathf.Abs(x) + Mathf.Abs(y));
        input.y = y / (Mathf.Abs(x) + Mathf.Abs(y));

        Move(input);

        Rotate(player.transform.position);

        if (timeSinceLastFire>timeBetweenFires)
        {
            Fire(0);
            timeSinceLastFire = 0;
        }
        timeSinceLastFire += Time.deltaTime;
    }

    void Fire(float direction)
    {
        Quaternion newRotation = transform.rotation;
        Vector3 currentAngles = transform.rotation.eulerAngles;
        currentAngles.z += direction;
        newRotation.eulerAngles = currentAngles;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, newRotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
}
