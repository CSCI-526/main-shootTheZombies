using UnityEngine;

public class ZombieBullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 15;
    private Vector3 targetPosition;

    public void Initialize(Vector3 target, int damage)
    {
        this.targetPosition = target;
        this.damage = damage;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            TowerBase tower = other.GetComponent<TowerBase>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    public void destroySelf(){
        Destroy(gameObject);
    }
}
