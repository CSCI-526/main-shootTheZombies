using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 2f;

    public int burstCount = 1;
    public float burstInterval = 0.1f;
    public int parallelCount = 1;
    public float parallelSpacing = 0.5f;

    public GameObject bulletGuideLinePrefab;
    public bool showGuideInTutorial = false;
    public float maxTime = 5f;

    public Sprite[] bulletSprites;

    private Color bulletColor;
    private Sprite currentBulletSprite;
    private float timer = 0f;
    private bool stop = false;
    private float nextFireTime = 0f;
    private float newFireRate = 3f;

    void Start()
    {
        if (bulletSprites != null && bulletSprites.Length >= 1)
            currentBulletSprite = bulletSprites[0];
        bulletColor = Color.red;
        // StartCoroutine(ShootCoroutine());
        // showGuideInTutorial = (SceneManager.GetActiveScene().buildIndex == 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bulletColor = Color.red;
            // currentBulletSprite = (bulletSprites.Length > 0 ? bulletSprites[0] : currentBulletSprite);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            bulletColor = Color.green;
            // currentBulletSprite = (bulletSprites.Length > 1 ? bulletSprites[1] : currentBulletSprite);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            bulletColor = Color.blue;
            // currentBulletSprite = (bulletSprites.Length > 2 ? bulletSprites[2] : currentBulletSprite);
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Vector3 target = GetMouseWorldPosition();
            FireBullet(target);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Vector3 target = GetMouseWorldPosition();
            FireBullet(target);
            float adjustedInterval = Mathf.Pow(0.9f, fireRate - 1);
            yield return new WaitForSeconds(adjustedInterval);
        }
    }

    bool IsZombieVisibleOnScreen()
    {
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        Camera cam = Camera.main;
        foreach (Zombie zombie in zombies)
        {
            Vector3 vp = cam.WorldToViewportPoint(zombie.transform.position);
            if (vp.z > 0 && vp.x >= 0 && vp.x <= 1 && vp.y >= 0 && vp.y <= 1)
                return true;
        }
        return false;
    }

    void FireBullet(Vector3 target)
    {
        // Debug.Log("FireBullet: showGuideInTutorial = " + showGuideInTutorial);
        if (showGuideInTutorial && bulletGuideLinePrefab != null && !stop)
        {
            // Debug.Log("FireBullet: Instantiate bulletGuideLinePrefab");
            // timer += Time.deltaTime;
            // if (timer >= maxTime) stop = true;
            GameObject guide = Instantiate(bulletGuideLinePrefab);
            var guideScript = guide.GetComponent<BulletGuideLine>();
            guideScript.bulletStart = transform;
        }

        if (!IsZombieVisibleOnScreen())
            return;

        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        StartCoroutine(BurstFire(transform.position, Quaternion.Euler(0, 0, angle)));
        SendAccuracy.bulletsFired += 1;
    }

    IEnumerator BurstFire(Vector3 pos, Quaternion rot)
    {
        for (int i = 0; i < burstCount; i++)
        {
            CreateParallelBullets(pos, rot);
            yield return new WaitForSeconds(burstInterval);
        }
    }

    void CreateParallelBullets(Vector3 pos, Quaternion rot)
    {
        float start = -(parallelCount - 1) * parallelSpacing / 2f;
        for (int i = 0; i < parallelCount; i++)
        {
            Vector3 spawnPos = pos + new Vector3(start + i * parallelSpacing, 0, 0);
            CreateBullet(spawnPos, rot);
        }
    }

    void CreateBullet(Vector3 position, Quaternion rotation)
    {
        var bullet = Instantiate(bulletPrefab, position, rotation);
        var bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.Initialize(position, bulletColor, null);

        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 d = rotation * Vector2.up;
            if ((d.x / d.y > 7.6f / 3.25f || d.y < 0) && d.x > 0) { d.x = 7.6f; d.y = 3.25f; }
            if ((d.x / d.y < -7.6f / 3.25f || d.y < 0) && d.x < 0) { d.x = -7.6f; d.y = 3.25f; }
            d.Normalize();
            rb.linearVelocity = d * bulletSpeed;
        }

        var sr = bullet.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            if (currentBulletSprite != null)
                sr.sprite = currentBulletSprite;
            sr.color = bulletColor;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        var mp = Input.mousePosition;
        mp.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mp);
    }
}
