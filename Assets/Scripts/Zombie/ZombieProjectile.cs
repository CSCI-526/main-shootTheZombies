using UnityEngine;

public class ZombieProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    private Vector3 direction;

    public void Initialize(Vector3 shootDirection, int damage)
    {
        this.damage = damage;
        this.direction = shootDirection.normalized;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            //Debug.LogError("ZombieProjectile Rigidbody2D is missing on this projectile!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Wall playerWall = collision.GetComponent<Wall>();
            if (playerWall != null)
            {
                playerWall.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
