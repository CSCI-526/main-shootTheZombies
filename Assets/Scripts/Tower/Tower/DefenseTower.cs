using UnityEngine;

public class DefenseTower  : TowerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireRate = 1;
        towerPosition = transform.position;
        StartCoroutine(ShootCoroutine());  
    }

    // Update is called once per frame
    public override void ShootBullet(Vector3 targetPosition, float speed, int damage)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        DefenseBullet bulletScript = bullet.GetComponent<DefenseBullet>();
        if (bulletScript != null)
        {
            Vector3 oppositePosition = towerPosition;
            oppositePosition.x = -towerPosition.x;
            bulletScript.Initialize(oppositePosition, speed, damage);
        }

    }
}
