using UnityEngine;

public class DefenseBullet : BulletBase
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ZombieBullet"))
        {
            ZombieBullet zombieBullet = other.GetComponent<ZombieBullet>();
            if (zombieBullet != null)
            {
                // Destroy(zombieBullet);
                zombieBullet.destroySelf();
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
