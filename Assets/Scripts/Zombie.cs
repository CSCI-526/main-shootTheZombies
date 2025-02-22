using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health = 90;
    public float moveSpeed = 2f;
    public int damage = 30;
    private float attackSpeed = 2f;
    private float attackTimer = 0f;
    private bool isAttacking = false;

    public GameObject damagePopupPrefab;

    private void Update()
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

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        GameObject canvasObj = GameObject.Find("Damage");

        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damageAmount);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isAttacking = true;
        }
    }

    private void AttackWall()
    {
        Wall wall = FindObjectOfType<Wall>();
        if (wall != null)
        {
            wall.TakeDamage(damage);
        }
    }
}
