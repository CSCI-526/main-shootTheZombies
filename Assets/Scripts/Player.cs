using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public TowerPlayerManager towerPlayerManager;
    public ButtonSpawner buttonSpawner;
    public int playerLevel = 1;
    public int exp = 0;
    public int expRate = 10;    // EXP gained per second
    private float timer = 0f;   // Timer to track elapsed time
    public enum TowerAttribute {hp,fireRate,bulletSpeed, bulletDamage, number };
    // public Dictionary<TowerAttribute, string> attributeToButtonText = new Dictionary<TowerAttribute, string>
    // {
    //     { TowerAttribute.hp, "Increase Tower HP" },
    //     { TowerAttribute.fireRate, "Boost Tower Fire Rate" },
    //     { TowerAttribute.bulletSpeed, "Speed Up Tower Bullets" },
    //     { TowerAttribute.bulletDamage, "Increase Tower Damage" },
    //     { TowerAttribute.number, "Increase Tower Number" }
    // };
    // 1 : Increase Tower HP
    // 2 : Boost Tower Fire Rate
    // 3 : Speed Up Tower Bullets
    // 4 : Increase Tower Damage
    // 5 : Increase Tower Number
    private List<string> buttoonTexts = new List<string>{"Increase Tower HP", "Boost Tower Fire Rate", "Speed Up Tower Bullets", "Increase Tower Damage", "Increase Tower Number", "Upgrade The Player Damage"};
    
    
         void Start()
    {
        towerPlayerManager = new TowerPlayerManager(); 
        buttonSpawner = FindObjectOfType<ButtonSpawner>();

        StartCoroutine(ShootCoroutine()); 
        // buttonSpawner.InitializeButtons("Increase Tower HP", "Boost Tower Fire Rate", "Speed Up Tower Bullets");
    }


    void Update()
    {
        //ExpGrowth();
    }

    private void ExpGrowth()
    {
        timer += Time.deltaTime; // Accumulate time

        if (timer >= 1f) // Every second
        {
            GainExp(expRate); // Gain EXP
            timer = 0f; // Reset timer
            //Debug.Log($"EXP: {exp}");
        }
    }

    public void GainExp(int amount)
    {
        exp += amount;
        // Debug.Log($"EXP: {exp}");
        if(this.exp >= 100){
            LevelUp();
            exp = 0;
        }
    }

    void LevelUp(){
        playerLevel += 1;
        Debug.Log("Level Up! Current Level: " + playerLevel);
        towerPlayerManager.UnlockTowerType(playerLevel);
        AutoGenerateButton();
        Time.timeScale = 0;
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
    public void PlaceTower(){

    }

    

//     public void AddTower(GameObject tower){
//             if(tower != null){
//                 currentTowerNumber += 1;
//             }
//             towerSpawner.CreateNewTower();
//         }


//     public void upgradeTower(GameObject tower,TowerAttribute attribute, int inc){
//             if(tower != null){
//                 switch(attribute){
//                     case TowerAttribute.damage:
//                         tower.GetComponent<TowerBase>().bulletDamage += inc;
//                         break;
//                     case TowerAttribute.range:
//                         tower.GetComponent<TowerBase>().range += inc;
//                         break;
//                     case TowerAttribute.fireRate:
//                         tower.GetComponent<TowerBase>().fireRate -= inc;
//                         break;
//                     default:
//                         break;
//                 }
//             }

    }



    

