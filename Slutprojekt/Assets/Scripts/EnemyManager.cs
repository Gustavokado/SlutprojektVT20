using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{   
    [SerializeField]
    List<string> names = new List<string>();
    Queue<string> enemyNames = new Queue<string>();
    Dictionary<EnemyType, int> enemies = new Dictionary<EnemyType, int>();
    [SerializeField]
    int rocketAmount;
    [SerializeField]
    int ninjaAmount;
    [SerializeField]
    Enemy rocketPrefab;
    [SerializeField]
    Enemy ninjaPrefab;

    float timeSinceLastCheck;
    [SerializeField]
    float timeBetweenChecks;

    private void Start()
    {
        enemies.Add(EnemyType.rocket, 0); 
        enemies.Add(EnemyType.ninja, 0);

        for (int i = 0; i < names.Count; i++)
        {
            enemyNames.Enqueue(names[i]);
        }
    }
    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck>=timeBetweenChecks)
        {
            if (enemies[EnemyType.rocket]<rocketAmount)
            {
                int amount = enemies[EnemyType.rocket];
                for (int i = 0; i < (rocketAmount - amount); i++)
                {
                    Enemy newRocket = Instantiate(rocketPrefab);
                    newRocket.SetName(enemyNames.Dequeue());
                    enemies[EnemyType.rocket]++;
                }
            }

            if (enemies[EnemyType.ninja] < ninjaAmount)
            {
                int amount = enemies[EnemyType.ninja];
                for (int i = 0; i < (ninjaAmount - amount); i++)
                {
                    Enemy newNinja = Instantiate(ninjaPrefab);
                    newNinja.SetName(enemyNames.Dequeue());
                    enemies[EnemyType.ninja]++;
                }
            }
            timeSinceLastCheck = 0;
        }        
    }

    public void AddDeadName(string newName)
    {
        enemyNames.Enqueue(newName);
    }

    public void RemoveEnemyFromDictionary(EnemyType type)
    {
        enemies[type]--;
    }

    public enum EnemyType
    {
        rocket,
        ninja
    }

}
