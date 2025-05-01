using UnityEngine;

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

            default:
                Debug.Log("All tutorial stages complete.");
                break;
        }

        if (currentStage != BulletTutorialStage.none)
        {
            var spawner = GameObject.Find("ZombieSpawner");
            if (spawner != null)
                spawner.GetComponent<TutorialZombieSpawner>().allowSpawning = false;
        }
        else
        {
            var spawner = GameObject.Find("ZombieSpawner");
            if (spawner != null)
                spawner.GetComponent<TutorialZombieSpawner>().allowSpawning = true;
        }
    }
}

public enum BulletTutorialStage
{
    BulletSwitching,
    AimingShooting,
    none
}