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

    private Color bulletColor = Color.red; 
    private Rigidbody2D rb;
    private bool hasReflected = false;

    public void Initialize(Vector3 target, Color color, Collider2D ignoreZombie = null)
    {
        targetPosition = target;
        ignoredZombie = ignoreZombie;
        bulletColor = color;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if (transform.position.x < -7.6f || transform.position.x > 7.6f || transform.position.y > 12.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == ignoredZombie) return;

        if (other.CompareTag("Zombie"))
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage, bulletColor);
            }

            SplitIntoSmallBullets(other);

            Destroy(gameObject); 
        }

        if (other.CompareTag("ReflectWall") && !hasReflected)
        {
            ReflectBullet(other);
            hasReflected = true;
            damage = 60;
        }
    }

    private void ReflectBullet(Collider2D wall)
    {
        Vector2 currentDirection = rb.linearVelocity.normalized;
        Vector2 reflectedDirection = Vector2.Reflect(currentDirection, wall.transform.right); 
        float angle = Mathf.Atan2(reflectedDirection.y, reflectedDirection.x) * Mathf.Rad2Deg - 90;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        rb.linearVelocity = reflectedDirection * rb.linearVelocity.magnitude;

        transform.rotation = rotation;
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
            // bulletScript.Initialize(position, ignoredZombie);  //for split, ignore the target zombie
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = rotation * Vector2.up;
            rb.linearVelocity = direction * splitSpeed;
        }
    }
}
