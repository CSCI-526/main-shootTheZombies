using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float speed;
    public int damage;
    public Vector3 targetPosition;  

    public void Initialize(Vector3 target, float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
        targetPosition = target;
        Vector3 direction = (targetPosition - transform.position).normalized;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; 
        }
    }

    public void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        if (transform.position.x < -7.6f || transform.position.x > 7.6f || transform.position.y > 10.0f)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                // print(damage);
                zombie.TakeDamage(damage, Color.black);
            }
            Destroy(gameObject); 
        }
    }

    public void setSpeed(float speed){
        this.speed = speed;
    }

    public void setDamage(int damage){
        this.damage = damage;
    }

}