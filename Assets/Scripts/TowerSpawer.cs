using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TowerSpawner : MonoBehaviour
{
    public TextMeshProUGUI hintText; 
    // public GameObject towerPrefab;
    public TowerDatabase towerDatabase;
    // public TowerBulletSpawner bulletSpawnerPrefab;
    private List<GameObject> towers = new List<GameObject>();
    private Player player;
    private TowerButtonSpawner towerButtonSpawner;
    private GameObject currentTower; // Declare currentTower as a class member
    private int MaxTowerNumber = 0;
    private int currentTowerNumber = 0;
    private float fixedX = 1f;
    private bool isMoving = false;
    // private int towerBuildTime = 30;
    private int towerType;
    private int currentTowerIndex = 0; // Add this line to track the current tower type
    private bool FirstTime = true;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialization if needed
        player = FindObjectOfType<Player>();
        towerButtonSpawner = FindObjectOfType<TowerButtonSpawner>();
        HideHint();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            Move();
        }   
        
    }
    public void incMaxTowerNumber(){
        MaxTowerNumber += 1;
    }
    public void AddTower(int type){
        //change isMoving
        //create tower
         MaxTowerNumber += 1;
        
        if(isMoving){
            DestroyCurrentTower();
        }
        towerType = type;
        
        CreateNewTower();//change ismoving inside

    }
    private void Move(){


        // Press right mouse button to change the tower type
        // if (Input.GetMouseButtonDown(1) && isMoving)
        // {
        //     ChangeTowerType();
        // }

        // Press left mouse button to place the tower
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = false;
            CreateNewTower();
            towerButtonSpawner.destoryHint();
        }

        if (currentTower != null && isMoving)
        {
            MoveTowerWithMouse(currentTower);
        }

    }
    
    private void CreateNewTower()
    {
        if(currentTowerNumber >= MaxTowerNumber){
            // ShowHint("You can not build more tower!");
            return ;
        }
        Debug.Log(towerType);
        currentTower = Instantiate(towerDatabase.towerPrefabs[towerType]);
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

    // private void ChangeTowerType()
    // {
    //     if (currentTower != null)
    //     {
    //         Destroy(currentTower);
    //     }

    //     currentTowerIndex = (currentTowerIndex + 1) % towerDatabase.towerPrefabs.Length;
    //     currentTower = Instantiate(towerDatabase.towerPrefabs[currentTowerIndex]);
    //     towers.Add(currentTower);
    //     isMoving = true;
    // }


    void MoveTowerWithMouse(GameObject tower )
    {
        float fixedX;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z; 
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Adjust fixedX based on mouse X position
        if (mousePosition.x < Screen.width / 2)
        {
            fixedX = -7f;
        }
        else
        {
            fixedX = 7f;
        }

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