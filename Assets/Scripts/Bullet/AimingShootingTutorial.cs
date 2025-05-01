using UnityEngine;
using TMPro;

public class AimingShootingTutorial : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;
    public GameObject keyHintLabelPrefab;

    private int targetsRemaining = 3;
    private bool tutorialStarted = false;

    public void StartTutorial()
    {
        tutorialStarted = true;
        promptText.text = "Move your mouse to aim and Left-Click to fire.";

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
        GameObject popup = Instantiate(keyHintLabelPrefab, headPos, Quaternion.identity, canvas);
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
        if (targetsRemaining <= 0)
        {
            promptText.text = "";
            BulletTutorialManager.Instance.AdvanceStage();
        }
    }
}
