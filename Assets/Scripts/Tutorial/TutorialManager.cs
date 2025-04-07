using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject meleeZombiePrefab;
    public GameObject explodingZombiePrefab;
    public GameObject rangedZombiePrefab;

    public Transform spawnPoint;
    public TutorialPopup tutorialPopup;

    private int tutorialStep = 0;
    private bool zombieAlive = false;

    private void Start()
    {
        StartTutorial();
    }

    private void Update()
    {
        if (!zombieAlive)
        {
            ProceedToNextStep();
        }
    }

    void StartTutorial()
    {
        tutorialPopup.ShowMessage("Press Q to switch to RED bullets!");
        SpawnZombie(meleeZombiePrefab);
    }

    void ProceedToNextStep()
    {
        tutorialStep++;

        switch (tutorialStep)
        {
            case 1:
                tutorialPopup.ShowMessage("Blue zombies explode! Press E for BLUE bullets!");
                SpawnZombie(explodingZombiePrefab);
                break;
            case 2:
                tutorialPopup.ShowMessage("Green zombies shoot from afar! Press W for GREEN bullets!");
                SpawnZombie(rangedZombiePrefab);
                break;
            case 3:
                tutorialPopup.ShowMessage("You're ready! Good luck!");
                break;
        }
    }

    void SpawnZombie(GameObject prefab)
    {
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        zombieAlive = true;
    }

    public void OnZombieKilled()
    {
        zombieAlive = false;
    }
}

// using UnityEngine;

// public class TutorialManager : MonoBehaviour
// {
//     public GameObject meleeZombiePrefab;
//     public GameObject explodingZombiePrefab;
//     public GameObject rangedZombiePrefab;

//     public BulletSpawner bulletSpawner;
//     public PlayerManagerScript playerManager;

//     private int tutorialStep = 0;
//     private bool isWaitingForKill = false;

    

//     private void Start()
//     {
//         ShowStep(0);
//     }

//     void ShowStep(int step)
//     {
//         isWaitingForKill = true;

//         switch (step)
//         {
//             case 0:
//                 playerManager.ShowPopup("A red zombie has appeared! Press Q to switch to red bullets.");
//                 SpawnZombie(meleeZombiePrefab);
//                 break;
//             case 1:
//                 playerManager.ShowPopup("Blue Exploding zombie! Press E to switch to blue bullets. Watch out, it explodes!");
//                 SpawnZombie(explodingZombiePrefab);
//                 break;
//             case 2:
//                 playerManager.ShowPopup("Green Ranged zombie! Press W to switch to green bullets.");
//                 SpawnZombie(rangedZombiePrefab);
//                 break;
//             case 3:
//                 playerManager.ShowPopup("You’ve mastered zombie colors! Get ready for real waves!");
//                 break;
//         }
//     }

//     void SpawnZombie(GameObject prefab)
//     {
//         Vector3 spawnPos = new Vector3(0, 6f, 0f);
//         GameObject zombie = Instantiate(prefab, spawnPos, Quaternion.identity);
//         TutorialZombie tutorialZombie = zombie.GetComponent<TutorialZombie>();
//         tutorialZombie.SetTutorialManager(this);
//     }

//     public void OnZombieKilled()
//     {
//         if (isWaitingForKill)
//         {
//             isWaitingForKill = false;
//             tutorialStep++;
//             Invoke(nameof(ContinueTutorial), 1.5f);
//         }
//     }

//     void ContinueTutorial()
//     {
//         ShowStep(tutorialStep);
//     }
// }