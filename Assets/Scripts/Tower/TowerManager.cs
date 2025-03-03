using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TowerDatabase towerDatabase;
    void Start()
    {


        GameObject basicTower = Instantiate(towerDatabase.towerPrefabs[0]);
        basicTower.transform.position = new Vector3(-7, 2, 0);

        // how to init
        GameObject flameTower = Instantiate(towerDatabase.towerPrefabs[1]);
        // GameObject flameTower = Instantiate(towerDatabase.towerPrefabs[1], /* POSITION you need to place tower at */, Quaternion.identity);
        flameTower.transform.position = new Vector3(7, 2, 0);
        // how to edit attributes
        FlameThrowerTower towerScript = flameTower.GetComponent<FlameThrowerTower>();
        if (towerScript != null)
        {
            towerScript.fireRate = 0.5f; 
            towerScript.bulletDamage = 1;
            towerScript.bulletSpeed = 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
