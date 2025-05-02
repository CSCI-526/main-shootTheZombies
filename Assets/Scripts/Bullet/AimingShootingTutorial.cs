using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class AimingShootingTutorial : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;
    public GameObject damagePopupPrefab;
    
    private BulletSpawner spawner; 
    private int targetsRemaining = 3;
    private bool tutorialStarted = false;
    private List<GameObject> hintLabels = new List<GameObject>();
    void Awake()
    {
        spawner = FindObjectOfType<BulletSpawner>();
    }

    public void StartTutorial()
    {
        tutorialStarted = true;
        promptText.text = "Move your mouse to aim and Left-Click to fire.";
        StartCoroutine(ClearPromptAfterDelay(1f));

        if (spawner != null)
            spawner.showGuideInTutorial = true;

        SpawnZombie(meleeZombiePrefab, new Vector3(-4f,6f,0f), "Q", Color.red);
        SpawnZombie(rangedZombiePrefab, new Vector3(0f,6f,0f), "W", Color.green);
        SpawnZombie(explodingZombiePrefab, new Vector3(4f,6f,0f), "E", Color.blue);
    }

    void SpawnZombie(GameObject prefab, Vector3 position, string key, Color color)
    {
        GameObject zombie = Instantiate(prefab, position, Quaternion.identity);
        var tz = zombie.GetComponentInChildren<Zombie>();
        if (tz == null) return;
        tz.hp = 30;
        tz.isTutorialTarget = true;
        tz.tutorialRef = this;

        GameObject canvasObj = GameObject.Find("Damage");
        Transform canvas = canvasObj.transform;
        Vector3 headPos = zombie.transform.position + Vector3.up * 2f;
        GameObject popup = Instantiate(damagePopupPrefab, headPos, Quaternion.identity, canvas);
        hintLabels.Add(popup);
        var textMesh = popup.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text  = key;
            textMesh.color = color;
            textMesh.fontSize = 50;
        }
    }

    public void OnZombieKilled()
    {
        targetsRemaining--;

        if (hintLabels.Count > 0)
        {
            Destroy(hintLabels[0]);
            hintLabels.RemoveAt(0);
        }

        if (targetsRemaining <= 0)
        {
            promptText.text = "";
            if (spawner != null)
                spawner.showGuideInTutorial = false;
            BulletTutorialManager.Instance.AdvanceStage();
        }
    }

    private IEnumerator ClearPromptAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        promptText.text = "";
    }
}
