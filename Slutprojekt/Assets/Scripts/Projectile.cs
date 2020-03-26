using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float direction;
    public float maxLifetime;
    float lifeTime =0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime>=maxLifetime)
        {
            Destroy(this.gameObject);
        }
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
