using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TowerSpawner : MonoBehaviour
{
    public TextMeshProUGUI hintText; 
    // public GameObject towerPrefab;
    public TowerDatabase towerDatabase;
    // public TowerBulletSpawner bulletSpawnerPrefab;
    private List<GameObject> towers = new List<GameObject>();
    private GameObject currentTower; // Declare currentTower as a class member
    private int MaxTowerNumber = 5;
    private int currentTowerNumber = 0;
    private float fixedX = 1f;
    private bool isMoving = true;
    // private int towerBuildTime = 30;
    private int currentTowerIndex = 0; // Add this line to track the current tower type

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialization if needed
        HideHint();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.time < towerBuildTime) return;
        // if(Time.time >= towerBuildTime && Time.time <= towerBuildTime + 1) ShowHint("Press 'Q' to build tower on the left side of the lane! Press 'E' on the right side. ");
        // Press E to place the tower on the right side
        KeyboardAction();
    }
    public void incMaxTowerNumber(){
        MaxTowerNumber += 1;
    }
    private void KeyboardAction(){
         if (Input.GetKeyDown(KeyCode.E))
        {
            fixedX = 7f;
            // if (isMoving)
            // {
            //     DestroyCurrentTower();
            
            // }
            //CreateNewTower();
        }
        // Press Q to place the tower on the left side
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            
           fixedX = -7f;
            // if (isMoving)
            // {
            //     DestroyCurrentTower();
            
            // }
            //CreateNewTower();
            
        }

        // Press Z to change the tower type
        if (Input.GetKeyDown(KeyCode.Z) && isMoving)
        {
            ChangeTowerType();
        }

        // Press left mouse button to place the tower
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = false;
            CreateNewTower();
        }

        if (currentTower != null && isMoving)
        {
            MoveTowerWithMouse(currentTower, fixedX);
        }

    }
    

    private void CreateNewTower()
    {
        if(currentTowerNumber >= MaxTowerNumber){
            ShowHint("You can not build more tower!");
            return ;
        }
        currentTower = Instantiate(towerDatabase.towerPrefabs[0]);
        // currentTower = Instantiate(towerPrefab);
        towers.Add(currentTower);
        currentTowerNumber += 1;
        isMoving = true;
    }

    private void DestroyCurrentTower()
    {
        if (currentTower != null)
        {
            Destroy(currentTower);
            currentTowerNumber -= 1;
        }
    }

    private void ChangeTowerType()
    {
        if (currentTower != null)
        {
            Destroy(currentTower);
        }

        currentTowerIndex = (currentTowerIndex + 1) % towerDatabase.towerPrefabs.Length;
        currentTower = Instantiate(towerDatabase.towerPrefabs[currentTowerIndex]);
        towers.Add(currentTower);
        isMoving = true;
    }


    void MoveTowerWithMouse(GameObject tower, float fixedX)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        worldPosition.x = fixedX; // Keep X locked
        worldPosition.z = 0f;     // Ensure Z is zero in 2D

        tower.transform.position = worldPosition;
    }

    public void ShowHint(string message)
    {
        hintText.text = message;
        CancelInvoke(nameof(HideHint)); 
        Invoke(nameof(HideHint), 3f);
    }
    void HideHint()
    {
        hintText.text = "";
    }
}