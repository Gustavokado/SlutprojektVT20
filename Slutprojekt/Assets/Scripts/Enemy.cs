using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{    
    string enemyName; //lite förvirrande men det är själva instansens namn, den gillade inte att variabeln hette "name" då det redan fanns nåt annat som hette det, kom inte på något bättre namn
    protected GameObject player; //referens till spelaren
    protected float distanceToPlayer;
    protected float timeSinceLastFire;
    [SerializeField]
    protected float timeBetweenFires;
    protected bool followPlayer = true; //bestämmer ifall enemyn för tillfället ska fälja efter spelaren eller cirkulera runt den  
    EnemyManager manager;
    EnemyManager.EnemyType type;
    [SerializeField]
    Text namePrefab; //referens till prefaben som ska skapas
    Text nameTag; //dens faktiska nametag
    Canvas canvas; //referens till canvas, behövs för att grejer med texten ska funka
    protected override void Start()
    {
        base.Start(); //behöver fortfarande det som bas start gör men behöver också lägga till nya referenser
        player = GameObject.Find("Player");
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        nameTag = Instantiate(namePrefab, transform.position, transform.rotation); //var tvungen att söka upp det mesta kring hur text funkar
        nameTag.transform.SetParent(canvas.transform, false); //för att textobjektet ska synas måste vara ett child av canvas
        nameTag.text = enemyName;
    }

    private void LateUpdate() //används just nu bara för att flytta texten, resten av funktionaliteten händer i subklassernas updatemetod
    {
        Vector3 position = transform.position;
       
        position.y += 1;
        
        nameTag.rectTransform.position = position; //text använder tydligen rectTransform istället för transform      
    }

    protected void Fire(float direction) //kallas av subklassernas updatemetod när den ska skjuta, direction är den riktningen jämfört mot var enemyns riktning, en direction av 0 är rakt fram, 30 är 30 grader osv.
    {
        Quaternion newRotation = transform.rotation;
        Vector3 currentAngles = transform.rotation.eulerAngles;
        currentAngles.z += direction;
        newRotation.eulerAngles = currentAngles; //ser till att projectilens framåtriktning är korrekt
        
        Projectile projectile = Instantiate(projectilePrefab, transform.position, newRotation);
        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>()); //ser till att projectilen inte kolliderar med enemyn
    }

    protected void CalculateInputTowardsPlayer() //används för att få rätt input som sedan ska in i character-metoden move
    {
        //resultatet av spelarens input är alltid mellan -1 och 1 på båda axlarna så det är det den här metoden försöker få fram
        //det den gör är att först få fram rätt riktning genom att ta skillnaden mellan sin och spelarens position
        //sen tar den reda på var på en cirkel med radien 1 den riktningen skulle hamna på
        //metoden tar också reda på hur långt det är till spelaren
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        if (x > 0) //för beräkningen med avståndet ska längderna vara upphöjt till två men för beräkningen av input måste skillnaderna bevara ifall det är negativt
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

        distanceToPlayer = Mathf.Sqrt(Mathf.Abs(x) + Mathf.Abs(y)); //pythagoras sats

        input.x = x / (Mathf.Abs(x) + Mathf.Abs(y)); //efter lite experimenterande och visualisering i paint kom jag fram till den här lösningen
        input.y = y / (Mathf.Abs(x) + Mathf.Abs(y));
    }

    protected void RotateInput() //ifall enemyn är sagd att cirkulera runt spelaren vrids input-vectorn 90 grader
    {
        float x = input.x;
        float y = input.y;
        
        input.x = y;
        input.y = x * -1; //igen efter en del visualisering och testning i paint kom jag på att det här är en enkel och generell lösning
    }

    public void SetName(string name) //kallas när en enemy skapas av enemymanagern för att ge den ett namn från kön
    {
        enemyName = name;
    }

    protected void OnDestroy() //kallas automatiskt när enemyn förstörs och gör lite arbete för managern samt förstör texten
    {
        manager.RemoveEnemyFromDictionary(type);
        manager.AddDeadName(enemyName);
        Destroy(nameTag.gameObject);
    }
}
