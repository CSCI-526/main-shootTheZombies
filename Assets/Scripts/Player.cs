using UnityEngine;
using System.Collections;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public TowerPlayerManager towerPlayerManager;
    public int playerLevel;
    public float bulletSpeed = 10f; 
    public float fireRate = 1f; 
    private Zombie targetZombie; 
    
    
         void Start()
    {
        towerPlayerManager = new TowerPlayerManager(); 

        StartCoroutine(ShootCoroutine()); 
    }


    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))  
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            
            Zombie clickedZombie = FindZombieAtPosition(mousePosition);
            
            if (clickedZombie != null)
            {
                targetZombie = clickedZombie; 
            }
        }

        if (targetZombie == null || targetZombie.health <= 0)
        {
            targetZombie = FindNearestZombie(transform.position);
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (targetZombie != null)
            {
                FireBullet(targetZombie.transform.position);
            }
            yield return new WaitForSeconds(fireRate); 
        }
    }

    void LevelUp(){
        playerLevel += 1;
        
    }
    void FireBullet(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized; 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // rotate
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle+90));

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(target);
    }

    Zombie FindZombieAtPosition(Vector3 position)
    {
        float detectionRadius = 1.5f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Zombie"))
            {
                return collider.GetComponent<Zombie>();
            }
        }
        return null;
    }

    Zombie FindNearestZombie(Vector3 playerPosition)
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        Zombie nearestZombie = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Zombie zombie in zombies)
        {
            float distance = Vector3.Distance(playerPosition, zombie.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestZombie = zombie;
            }
        }

        return nearestZombie;
    }
}


public class PlayerShooter{

}

 public class TowerPlayerManager {

    private int maxTowerNumber;
    private int currentTowerNumber;
    // private TowerDataBase towerDataBase;
//     private list<bool> level2UnlockTowerType = new list<int> { true,false,false,true,false,false};


//     public TowerSpawner towerSpawner;


//     private enum TowerAttribute {hp,fireRate,bulletSpeed, bulletDamage };
     
    public TowerPlayerManager(){
        maxTowerNumber = 0;
        currentTowerNumber = 0;
        InitializeDisplayTowerUnlocked();

    }
    private void InitializeDisplayTowerUnlocked(){
        // display tower unlocked
        // first for non unlock tower
        // for(int i = 0; i < towerDataBase.towerData.Count; i++){
        //     if(towerDataBase.towerData[i].level == 0){
        //         // display tower locked
        //     }
        // }
    }


//     public void UnlockTowerType(int level){
//         if(level2UnlockTowerType[level]){
//             // unlock tower type
//         }
//     }

    public void DisplayTowerUnlocked(){
        
    }

//     public void DisplayTowerAvailable(){

//     }

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



    

