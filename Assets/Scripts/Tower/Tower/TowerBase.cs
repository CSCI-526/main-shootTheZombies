using UnityEngine;
using System.Collections;

public class TowerBase : MonoBehaviour
{
    // basic attributes
    public float hp = 100;
    public float fireRate = 1.0f;    // 发射间隔（秒）
    public float bulletSpeed = 5f;   // 子弹速度
    public int bulletDamage = 30;
    // public float range = 10.0f;
    public Vector3 towerPosition; 
    public Vector3 targetPosition;  // 目标位置
    
    // Prefabs
    public GameObject bulletPrefab;  // 子弹预制体
    public GameObject damagePopupPrefab;

    public TowerManager towerManager;
    public ButtonSpawner buttonSpawner;

    
   


    void Start()
    {
        if (GetComponent<BoxCollider2D>() == null)
    {
        gameObject.AddComponent<BoxCollider2D>(); // Ensure collider exists
    }
        print(gameObject);
        towerPosition = transform.position;
        StartCoroutine(ShootCoroutine());
        print(bulletPrefab);
        
    }


    public IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Zombie nearestZombie = FindNearestZombie(towerPosition);
            if (nearestZombie != null)
            {
                // print("Nearest zombie: " + nearestZombie);
                ShootBullet(nearestZombie.transform.position, bulletSpeed, bulletDamage);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void OnMouseDown()
    {
        // towerManager = FindObjectOfType<TowerManager>();
        buttonSpawner = FindObjectOfType<ButtonSpawner>();
        print(buttonSpawner);
        // print("Selected tower type: " + this);
        buttonSpawner.SelectTower(this);
        //log
        
        // towerManager.towerDatabase.towerPrefabs[0];
    }


    public virtual void ShootBullet(Vector3 targetPosition, float speed, int damage)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        BasicBullet bulletScript = bullet.GetComponent<BasicBullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(targetPosition, speed, damage);
            // bulletScript.setDamage(damage);
            // bulletScript.setSpeed(speed);
        }
    }

    public Zombie FindNearestZombie(Vector3 position)
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        Zombie nearestZombie = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Zombie zombie in zombies)
        {
            float distance = Vector3.Distance(position, zombie.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestZombie = zombie;
            }
        }
        return nearestZombie;
    }


    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        print("Tower 受到了 " + damage + " 点伤害，剩余 HP：" + hp);
        GameObject canvasObj = GameObject.Find("Damage");

        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damage);
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        print("Zombie 被击杀！");
        Destroy(gameObject);
    }
}
