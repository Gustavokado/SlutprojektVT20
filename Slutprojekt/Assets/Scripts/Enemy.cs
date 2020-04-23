using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{    
    string enemyName;
    protected GameObject player;
    protected float distanceToPlayer;
    protected float timeSinceLastFire;
    [SerializeField]
    protected float timeBetweenFires;
    protected bool followPlayer = true;   
    EnemyManager manager;
    EnemyManager.EnemyType type;
    [SerializeField]
    Text namePrefab;
    Text nameTag;
    Canvas canvas;
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        nameTag = Instantiate(namePrefab, transform.position, transform.rotation);
        nameTag.transform.SetParent(canvas.transform, false);
        nameTag.text = enemyName;
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
       
        position.y += 1;
        
        nameTag.rectTransform.position = position;
        
    }

    protected void Fire(float direction)
    {
        Quaternion newRotation = transform.rotation;
        Vector3 currentAngles = transform.rotation.eulerAngles;
        currentAngles.z += direction;
        newRotation.eulerAngles = currentAngles;

        Projectile projectile = Instantiate(projectilePrefab, transform.position, newRotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    protected void CalculateInputTowardsPlayer()
    {
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        if (x > 0)
        {
            x = Mathf.Pow(x, 2);
        }
        else
        {
            x = Mathf.Pow(x, 2) * -1;
        }
        if (y > 0)
        {
            y = Mathf.Pow(y, 2);
        }
        else
        {
            y = Mathf.Pow(y, 2) * -1;
        }

        distanceToPlayer = Mathf.Sqrt(Mathf.Abs(x) + Mathf.Abs(y));

        input.x = x / (Mathf.Abs(x) + Mathf.Abs(y));
        input.y = y / (Mathf.Abs(x) + Mathf.Abs(y));
    }

    protected void RotateInput()
    {
        float x = input.x;
        float y = input.y;
        
        input.x = y;
        input.y = x * -1;
    }

    public void SetName(string name)
    {
        enemyName = name;
    }

    protected void OnDestroy()
    {
        manager.RemoveEnemyFromDictionary(type);
        manager.AddDeadName(enemyName);
        Destroy(nameTag.gameObject);
    }
}
