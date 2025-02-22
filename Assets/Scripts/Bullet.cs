using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f; 
    public int damage = 30;  
    private Vector3 targetPosition;  

    public void Initialize(Vector3 target)
    {
        targetPosition = target;
        Vector3 direction = (targetPosition - transform.position).normalized;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
        }
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
            Destroy(gameObject); 
        }
    }
}