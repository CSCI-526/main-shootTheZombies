using UnityEngine;
using System.Collections;

public class FlameThrowerTower : TowerBase
{

    void Start()
    {
        // fireRate = 3;
        towerPosition = transform.position;
        StartCoroutine(ShootCoroutine());  
    }

    public override void ShootBullet(Vector3 targetPosition, float speed, int damage)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        FlameBullet bulletScript = bullet.GetComponent<FlameBullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(targetPosition, speed, damage);
        }

    }


    public override void TakeDamage(int damage)
    {
        hp -= damage;
        print("FlameThrower 受到了 " + damage + " 点伤害，剩余 HP：" + hp);
        GameObject canvasObj = GameObject.Find("Damage");

        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damage);
        if (hp <= 0)
        {
            Die();
        }
    }
}
