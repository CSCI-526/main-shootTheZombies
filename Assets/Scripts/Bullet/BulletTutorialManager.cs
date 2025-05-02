using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletTutorialManager : MonoBehaviour
{
    public static BulletTutorialManager Instance;

    public BulletTutorialStage currentStage = BulletTutorialStage.BulletSwitching;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AdvanceStage()
    {
        if (SceneManager.GetActiveScene().name != "TutorialLevel") return;
        switch (currentStage)
        {
            case BulletTutorialStage.BulletSwitching:
                currentStage = BulletTutorialStage.AimingShooting;

                var aiming = FindObjectOfType<AimingShootingTutorial>();
                if (aiming != null)
                    aiming.StartTutorial();
                else
                    Debug.LogWarning("AimingShootingTutorial not found in scene!");
                break;

            case BulletTutorialStage.AimingShooting:
                currentStage = BulletTutorialStage.none;
                Debug.Log("Tutorial in aiming shooting stage.");
                break;

            case BulletTutorialStage.none:
                Debug.Log("Tutorial in none stage.");
                break;

            default:
                Debug.Log("Tutorial in default stage.");
                break;
        }

        if (currentStage != BulletTutorialStage.none)
        {
            var spawner = GameObject.Find("ZombieSpawner");
            if (spawner != null)
                spawner.GetComponent<ZombieSpawner>().allowSpawning = false;
        }
        else
        {
            var spawner = GameObject.Find("ZombieSpawner");
            if (spawner != null)
                spawner.GetComponent<ZombieSpawner>().allowSpawning = true;
        }
    }
}

public enum BulletTutorialStage
{
    BulletSwitching,
    AimingShooting,
    none
}