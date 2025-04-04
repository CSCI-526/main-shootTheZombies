using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{ 
    public int damage = 30;
    public int splitSpeed = 10;

    public GameObject splitBulletPrefab;
    public int splitCount = 3;    

    private Vector3 targetPosition;
    private Quaternion originalRotation;
    private Collider2D ignoredZombie;

    public void Initialize(Vector3 target, Collider2D ignoreZombie = null)
    {
        targetPosition = target;
        ignoredZombie = ignoreZombie;
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == ignoredZombie) return;

        if (other.CompareTag("Zombie"))
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                SpriteRenderer sr = bullet.GetComponent<SpriteRenderer>();
                private Color bulletColor = sr.color;
                zombie.TakeDamage(damage, bulletColor);
            }

            SplitIntoSmallBullets(other);

            Destroy(gameObject); 
        }
    }

    public void SetRotation(Quaternion rotation)
    {
        originalRotation = rotation;
    }

    private void SplitIntoSmallBullets(Collider2D hitZombie)
    {
        if (splitCount < 2 || splitBulletPrefab == null) return;

        int totalBullets = splitCount + 1; 
        float angleStep = 360f / totalBullets;

        for (int i = 1; i < totalBullets; i++)
        {
            float angleOffset = angleStep * i;
            Quaternion bulletRotation = originalRotation * Quaternion.Euler(0, 0, angleOffset) * Quaternion.Euler(0, 0, 180);

            CreateBullet(transform.position, bulletRotation, hitZombie, splitBulletPrefab);
        }
    }

    private void CreateBullet(Vector3 position, Quaternion rotation, Collider2D ignoredZombie, GameObject prefab)
    {

        GameObject bullet = Instantiate(prefab, position, rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Initialize(position, ignoredZombie);
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.up;
            rb.linearVelocity = direction * splitSpeed;
        }
    }
}
