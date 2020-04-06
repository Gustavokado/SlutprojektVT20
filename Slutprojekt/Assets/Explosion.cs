using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float damage;
    [SerializeField]
    float activeTime;    
    float lifeTime;
    float transparencySpeed = .01f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime>=activeTime)
        {
            GetComponent<Collider2D>().enabled = false;

            Color color = GetComponent<SpriteRenderer>().color;

            color.a -= transparencySpeed;
            if (color.a<=0)
            {
                Destroy(this.gameObject);
            }
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character hitCharacter = collision.GetComponent<Character>();
        hitCharacter.ReduceHealth(damage);
    }
}
