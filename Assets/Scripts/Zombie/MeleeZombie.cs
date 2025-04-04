using UnityEngine;

public class MeleeZombie : Zombie
{
    public float moveSpeed = 2f;
    private float attackSpeed = 2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    public Player player;
    public int damage = 30;

    protected override void Start()
    {
        hp = 100;
        color = Color.Red;
    }

    public void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0f;
                AttackWall();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isAttacking = true;
        }
    }

    public void AttackWall()
    {
        Wall wall = FindObjectOfType<Wall>();
        if (wall != null)
        {
            wall.TakeDamage(damage);
        }
    }
}
