using UnityEngine;

public class FreezeTower  : TowerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireRate = 3;
        towerPosition = transform.position;
        StartCoroutine(ShootCoroutine());  
    }

    // Update is called once per frame
    public override void ShootBullet(Vector3 targetPosition, float speed, int damage)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        FreezeBullet bulletScript = bullet.GetComponent<FreezeBullet>();
        if (bulletScript != null)
        {
            Vector3 oppositePosition = towerPosition;
            oppositePosition.x = -towerPosition.x;
            bulletScript.Initialize(oppositePosition, speed, damage);
        }

    }
}
