using UnityEngine;

public class TutorialZombieSpawner : MonoBehaviour
{
    public GameObject tutorialMeleeZombiePrefab;
    public GameObject tutorialRangedZombiePrefab;
    public GameObject tutorialExplodingZombiePrefab;
    private GameObject QBImg;
    private GameObject WBImg;
    private GameObject EBImg;

    private GameObject QWImg;
    private GameObject WWImg;
    private GameObject EWImg;
    //public float spawnDelay = 5f;
    public Vector2 spawnRangeX = new Vector2(-4f, 4f);
    public float fixedY = 10f;
    private float timeSinceLastSpawn = 0f;
    public float spawnInterval = 10f; // Time between spawns
    private bool firstSpawn = true;
    private int spawnStep = 0;
    //private bool hasSpawned = false;
    public bool allowSpawning = false;
    public GameObject keyHintLabelPrefab;

    public void Start()
    {
        QBImg = GameObject.Find("QB");
        WBImg = GameObject.Find("WB");
        EBImg = GameObject.Find("EB");

        QWImg = GameObject.Find("QW");
        WWImg = GameObject.Find("WW");
        EWImg = GameObject.Find("EW");

    }

    private void Update()
    {
        if (!allowSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;
        if (firstSpawn && timeSinceLastSpawn >= 5f)
        {
            SpawnNextZombie();
            firstSpawn = false;
            timeSinceLastSpawn = 0f;
        }
        else if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnNextZombie();
            timeSinceLastSpawn = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Detect Q key press
        {
            if (QWImg != null)
            {
                QWImg.GetComponent<SpriteRenderer>().sortingOrder = 0;
               
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) // Detect W key press
        {
            if (WWImg != null)
            {
                WWImg.GetComponent<SpriteRenderer>().sortingOrder = 0;
            
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) // Detect E key press
        {
            if (EWImg != null)
            {
                EWImg.GetComponent<SpriteRenderer>().sortingOrder = 0;
               
            }
        }
    }

    private void SpawnNextZombie()
    {
        if (!allowSpawning) return;
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector3 spawnPosition = new Vector3(randomX, fixedY, 0f);
        GameObject zombiePrefab;

        switch (spawnStep)
        {
            case 0:
                zombiePrefab = tutorialMeleeZombiePrefab;  // Red
                if (QWImg != null)
                {
                    QWImg.GetComponent<SpriteRenderer>().sortingOrder  = 11;
             
                }
                break;
            case 1:
                zombiePrefab = tutorialMeleeZombiePrefab;  // Red
                break;
            case 2:
                zombiePrefab = tutorialExplodingZombiePrefab;  // Blue
                if (QWImg != null)
                {
                    QWImg.GetComponent<SpriteRenderer>().sortingOrder  = 0;
             
                }
                if (EWImg != null)
                {
                    EWImg.GetComponent<SpriteRenderer>().sortingOrder  = 11;

                }
                break;
            case 3:
                zombiePrefab = tutorialExplodingZombiePrefab;  // Blue
                break;
            case 4:
                zombiePrefab = tutorialRangedZombiePrefab;  // Green
                if (EWImg != null)
                {
                    EWImg.GetComponent<SpriteRenderer>().sortingOrder  = 0;

                }
                if (WWImg != null)
                {
                    WWImg.GetComponent<SpriteRenderer>().sortingOrder  = 11;
                     Debug.Log("WWImg layer set to 11 after pressing E.");
                }
                break;
            case 5:
                zombiePrefab = tutorialRangedZombiePrefab;  // Green
                break;
            default:
                return;  // End of tutorial spawns
        }

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
        spawnStep++;
        //hasSpawned = false;
    }
}
