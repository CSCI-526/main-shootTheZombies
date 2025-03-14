using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // public GameObject bulletPrefab; 
    public TowerPlayerManager towerPlayerManager;
    public ButtonSpawner buttonSpawner;
    public int playerLevel = 1;
    public float bulletSpeed = 10f; 
    public float fireRate = 1f; 
    private Zombie targetZombie; 
    public int exp = 0;
    public int expRate = 10;    // EXP gained per second
    private float timer = 0f;   // Timer to track elapsed time

    // 1 : Tower HP
    // 2 : Tower Fire Rate
    // 3 : Tower Bullets
    // 4 : Tower Damage
    // 5 : Tower Number
    private List<string> buttoonTexts = new List<string>{"Tower HP", "Tower Fire Rate", "Tower Bullets", "Tower Damage", "Tower Number", "Player Damage"};
    
    
         void Start()
    {
        towerPlayerManager = new TowerPlayerManager(); 
        buttonSpawner = FindObjectOfType<ButtonSpawner>();

        // StartCoroutine(ShootCoroutine()); 
        // buttonSpawner.InitializeButtons("Increase Tower HP", "Boost Tower Fire Rate", "Speed Up Tower Bullets");
    }


    void Update()
    {
        //ExpGrowth();
        // if (Input.GetMouseButtonDown(0))  
        // {
            // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            
            // Zombie clickedZombie = FindZombieAtPosition(mousePosition);
            
        //     if (clickedZombie != null)
        //     {
        //         targetZombie = clickedZombie; 
        //     }
        // }

        // if (targetZombie == null || targetZombie.hp <= 0)
        // {
        //     targetZombie = FindNearestZombie(transform.position);
        // }
    }

    // IEnumerator ShootCoroutine()
    // {
    //     while (true)
    //     {
    //         if (targetZombie != null)
    //         {
    //             FireBullet(targetZombie.transform.position);
    //         }
    //         yield return new WaitForSeconds(fireRate); 
    //     }
    // }

    private void ExpGrowth()
    {
        timer += Time.deltaTime; // Accumulate time

        if (timer >= 1f) // Every second
        {
            GainExp(expRate); // Gain EXP
            timer = 0f; // Reset timer
            // //Debug.Log($"EXP: {exp}");
        }
    }

    public void GainExp(int amount)
    {
        exp += amount;
        // //Debug.Log($"EXP: {exp}");
        if(this.exp >= 100){
            LevelUp();
            exp = 0;
        }
    }

    void LevelUp(){
        playerLevel += 1;
        // //Debug.Log("Level Up! Current Level: " + playerLevel);
        towerPlayerManager.UnlockTowerType(playerLevel);
        AutoGenerateButton();
        
    }
    void AutoGenerateButton(){
        // randomly choose 3 int  without repeat
        HashSet<int> randomIndex = new HashSet<int>();
        while(randomIndex.Count < 3){
            randomIndex.Add(Random.Range(0, buttoonTexts.Count));
        }
        List<string> buttonTexts = new List<string>();
        foreach(int index in randomIndex){
            buttonTexts.Add(buttoonTexts[index]);
        }
        buttonSpawner.InitializeButtons(buttonTexts[0], buttonTexts[1], buttonTexts[2]);
    }
    // void FireBullet(Vector3 target)
    // {
    //     Vector3 direction = (target - transform.position).normalized; 
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //     // rotate
    //     GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle+90));

    //     Bullet bulletScript = bullet.GetComponent<Bullet>();
    //     bulletScript.Initialize(target);
    // }

    // Zombie FindZombieAtPosition(Vector3 position)
    // {
    //     float detectionRadius = 1.5f;
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(position, detectionRadius);

    //     foreach (Collider2D collider in colliders)
    //     {
    //         if (collider.CompareTag("Zombie"))
    //         {
    //             return collider.GetComponent<Zombie>();
    //         }
    //     }
    //     return null;
    // }

    // Zombie FindNearestZombie(Vector3 playerPosition)
    // {
    //     Zombie[] zombies = FindObjectsOfType<Zombie>();
    //     Zombie nearestZombie = null;
    //     float shortestDistance = Mathf.Infinity;

    //     foreach (Zombie zombie in zombies)
    //     {
    //         float distance = Vector3.Distance(playerPosition, zombie.transform.position);
    //         if (distance < shortestDistance)
    //         {
    //             shortestDistance = distance;
    //             nearestZombie = zombie;
    //         }
    //     }

    //     return nearestZombie;
    // }

}


public class PlayerShooter{

}

 public class TowerPlayerManager {

    private int maxTowerNumber;
    private int currentTowerNumber;
    public TowerDatabase towerDataBase;
    private HashSet<int> level2UnlockTowerType = new HashSet<int> { 2, 6 };


    public TowerSpawner towerSpawner;



     
    public TowerPlayerManager(){
        maxTowerNumber = 0;
        currentTowerNumber = 0;
    }
    
    
    public void UnlockTowerType(int level){           
        if(level2UnlockTowerType.Contains(level)){

        }
       
    
    }
    
 }




    

