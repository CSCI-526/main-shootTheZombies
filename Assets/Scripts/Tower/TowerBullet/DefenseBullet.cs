using UnityEngine;

public class DefenseBullet : BulletBase
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        var zp = other.GetComponent<ZombieProjectile>();
        if (zp != null)
        {
            // destroy the zombie bullet
            Destroy(other.gameObject);
            // destroy this defense bullet
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (transform.position.x < -7.6f || transform.position.x > 7.6f || transform.position.y > 12.0f)
        {
            Destroy(gameObject);
        }
    }
}
