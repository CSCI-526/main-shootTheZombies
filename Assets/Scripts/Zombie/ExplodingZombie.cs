using UnityEngine;
using UnityEngine.SceneManagement;
public class ExplodingZombie : Zombie
{
    public float explosionRadius = 3f;
    public int explosionDamage = 30;
    public float moveSpeed = 2f;
    private float attackSpeed = 2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    public int damage = 30;
    public GameObject explosionEffectPrefab;
    protected override void Start()
    {
        hp = 150;
        color = Color.blue;
    }

    public void Update()
    {
        if (isTutorialTarget) return;
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

    public virtual void TakeDamage(int damageAmount, Color bulletColor)
    {
        if (bulletColor != color && bulletColor != Color.black) return;
        hp -= damageAmount;
        //Debug.Log("Exploding Zombie took damage: " + damageAmount + ", HP: " + hp);

        GameObject canvasObj = GameObject.Find("Damage");
        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damageAmount);

        if (hp <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        //Debug.Log("Exploding Zombie Died");
        Explode();

        Player player = GameObject.Find("Testplayer").GetComponent<Player>();

        //Debug.Log("Destroying Exploding Zombie: " + gameObject.name);
        Destroy(gameObject);
        
        if (player.playerLevel <= 6)
        {
             if (SceneManager.GetActiveScene().name == "TutorialLevel"){
                player.GainExp(50);
             }else{
                player.GainExp(20);
             }
            
        }
    }

    private void Explode()
    {
        //Debug.Log("Exploding! Checking for nearby objects...");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        //Debug.Log("Total objects detected in explosion: " + colliders.Length);

        foreach (Collider2D col in colliders)
        {
            //Debug.Log("Found zombies in explosion radius: " + col.gameObject.name);

            if (col.CompareTag("Zombie"))
            {
                Zombie otherZombie = col.GetComponent<Zombie>();
                if (otherZombie != null && otherZombie != this)
                {
                    //Debug.Log("Damaging zombie: " + col.gameObject.name + " with " + explosionDamage + " damage");
                    otherZombie.TakeDamage(explosionDamage, Color.black);
                }
            }
            else if (col.CompareTag("Wall"))
            {
                Wall wall = col.GetComponent<Wall>();
                if (wall != null)
                {
                    //Debug.Log("Damaging wall: " + col.gameObject.name + " with " + explosionDamage + " damage");
                    wall.TakeDamage(explosionDamage);
                }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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