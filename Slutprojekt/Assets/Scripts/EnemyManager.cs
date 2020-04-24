using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{   
    [SerializeField]
    List<string> names = new List<string>(); //används bara för att man ska kunna skriva in namnen man vill ha i inspectorn, queues visas inte där av nån anledning
    Queue<string> enemyNames = new Queue<string>(); //har alla namnen som enemies kan ha, när en enemy dör läggs namnet till i kön och när en enemy skapas får den ett namn från kön
    Dictionary<EnemyType, int> enemies = new Dictionary<EnemyType, int>(); //används för att hålla koll på hur många av varje typ av enemy som finns
    [SerializeField]
    int rocketAmount; //den mängden av denna typ av fiende som managern ska se till att det finns vid varje check
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
        enemies.Add(EnemyType.rocket, 0); //ser till att dictionaryn är satt upp med de olika typerna av enemies när spelet börjar
        enemies.Add(EnemyType.ninja, 0);

        for (int i = 0; i < names.Count; i++) //lägger över alla namn från listan man ser i inspectorn till kön som faktikst använd i programmet
        {
            enemyNames.Enqueue(names[i]); 
        }
    }
    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        if (timeSinceLastCheck>=timeBetweenChecks) //varje x sekunder (man kan ändra) kollar den hur många av varje typ av enemy som existerar och skapar nya så att det finns så många man vill ska finnas
        {
            if (enemies[EnemyType.rocket]<rocketAmount)
            {
                int amount = enemies[EnemyType.rocket]; //man kan inte använda rocketAmount - enemies[EnemyType.rocket] direkt i loopen då värdet i dictionaryn ändras i loopen
                for (int i = 0; i < (rocketAmount - amount); i++)
                {
                    Enemy newRocket = Instantiate(rocketPrefab);
                    newRocket.SetName(enemyNames.Dequeue()); //tar namnet längst fram och ger det till den nya enemyn
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
            timeSinceLastCheck = 0; //resetar timern
        }        
    }

    public void AddDeadName(string newName) //kallas när en enmy dör och lägger till dess namn i kön
    {
        enemyNames.Enqueue(newName);
    }

    public void RemoveEnemyFromDictionary(EnemyType type) //kallas också när en enemy dör för att dicttionaryn ska uppdateras
    {
        enemies[type]--;
    }

    public enum EnemyType //ville prova att använda enums för att de verkar coola, används som index för dictionaryn
    {
        rocket,
        ninja
    }
}
