using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // public GameObject bulletPrefab; 
    public TowerPlayerManager towerPlayerManager;
    private ButtonSpawner buttonSpawner;
    private TowerButtonSpawner towerButtonSpawner;
    public int playerLevel = 1;
    public float bulletSpeed = 10f; 
    public float fireRate = 1f; 
    private Zombie targetZombie; 
    public int exp = 0;
    public int expRate = 10;    // EXP gained per second
    private float timer = 0f;   // Timer to track elapsed time
    private GameObject grayCover; // Reference to the gray cover UI element

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
        towerButtonSpawner = FindObjectOfType<TowerButtonSpawner>();
        grayCover = GameObject.Find("Cover"); // Find the gray cover in the scene
        Debug.Log("graycover11");
        if (grayCover != null)
        {
            Debug.Log("graycover");
            grayCover.SetActive(false); // Ensure the gray cover is initially hidden
        }
        // towerButtonSpawner.InitializeButtons();
        // LevelUp();

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
        Time.timeScale = 0;
        playerLevel += 1;
        if (grayCover != null)
        {
            grayCover.SetActive(true); // Show the gray cover
        }
        // //Debug.Log("Level Up! Current Level: " + playerLevel);
        towerPlayerManager.UnlockTowerType(playerLevel);
        // AutoGenerateButton();
        towerButtonSpawner.InitializeButtons();
        // Ensure the game remains stopped until ResumeGame is explicitly called
        return;
    }

    public void ResumeGame()
    {
        if (grayCover != null)
        {
            grayCover.SetActive(false); // Hide the gray cover
        }
        Time.timeScale = 1; // Resume the game
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






