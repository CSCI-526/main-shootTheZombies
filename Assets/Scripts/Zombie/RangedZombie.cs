using UnityEngine;
using UnityEngine.SceneManagement;

public class RangedZombie : Zombie
{
    public GameObject projectilePrefab;
    public float shootCooldown = 2f;
    public float moveSpeed = 2f;
    private float shootTimer = 0f;
    public int damage = 10;

    protected override void Start()
    {
        if (SceneManager.GetActiveScene().name == "TutorialLevel")
        {
            hp = 30;
        }
        else
        {
            hp = 100;
        }
        color = Color.green;
    }

    private void Update()
    {
        if (isTutorialTarget) return;
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            ShootPlayerWall();
            shootTimer = 0f;
        }
    }

    private void ShootPlayerWall()
    {
        Wall playerWall = FindObjectOfType<Wall>(); 

        if (projectilePrefab != null && playerWall != null)
        {
            Vector3 shootDirection = Vector3.down;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            ZombieProjectile projScript = projectile.GetComponent<ZombieProjectile>();

            if (projScript != null)
            {
                projScript.Initialize(shootDirection, damage);
            }
            else
            {
                //Debug.LogError("RangedZombie Projectile script is missing on the instantiated object!");
            }
        }
        else
        {
            //Debug.LogError("RangedZombie Projectile Prefab or Wall is missing!");
        }
    }
}
