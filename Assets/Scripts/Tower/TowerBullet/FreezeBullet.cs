using UnityEngine;

public class FreezeBullet : BulletBase
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie zombie = other.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage, Color.black);
                // zombie.ApplySlow(0.9, 3);
            }
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        if (transform.position.x < -7.6f || transform.position.x > 7.6f || transform.position.y > 10.0f)
        {
            Destroy(gameObject);
        }
    }
}
