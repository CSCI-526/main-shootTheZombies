using UnityEngine;
using TMPro;

public class AimingShootingTutorial : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;

    private int targetsRemaining = 3;
    private bool tutorialStarted = false;

    public void StartTutorial()
    {
        tutorialStarted = true;
        promptText.text = "Move your mouse to aim and Left-Click to fire.";

        SpawnZombie(meleeZombiePrefab, new Vector3(-2f, 10f, 0f));
        SpawnZombie(rangedZombiePrefab, new Vector3(0f, 10f, 0f));
        SpawnZombie(explodingZombiePrefab, new Vector3(2f, 10f, 0f));
    }

    void SpawnZombie(GameObject prefab, Vector3 position)
    {
        GameObject zombie = Instantiate(prefab, position, Quaternion.identity);
        Zombie tutorialZombie = zombie.GetComponent<Zombie>();
        if (tutorialZombie != null)
        {
            tutorialZombie.hp = 30;
            tutorialZombie.isTutorialTarget = true;
            tutorialZombie.tutorialRef = this;
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
