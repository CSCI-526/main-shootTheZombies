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
                // zombie.TakeDamage(damage);
                // zombie.ApplySlow(0.9, 3);
            }
            // Destroy(gameObject); 
        }
    }
}
