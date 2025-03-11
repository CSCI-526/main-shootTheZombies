using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
    //will change after we get the xp and level from others
    public int level = 1;
    public float xp = 0f;
    
    public GameObject LevelUpUI;
    public GameObject[] players; 
    private int index;

    public BulletSpawner bulletSpawner;
    public Bullet bulletPrefab;

    // BulletSpawner
    public float newFireRate = 5f;
    public int newBurstCount = 1;
    public int newParallelCount = 2;

    // Bullet
    public int newDamage = 20;
    public int newSplitCount = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject child = players[2].transform.Find("SimpleBullet").gameObject;
        //Debug.Log("找到子对象：" + child.name);
        //Debug.Log("找到对象：" +  players[2].name);
        
    }

    // Update is called once per frame
    void Update()
    {
        // add more player
        ChoosePlayer();

        //show the UI of choosing skills
        LevelUp();
        
        if(xp == 50){
            ModifyBulletProperties();
            xp = 0;
        }
    }



    public void LevelUp(){
        if(xp == 100){
            LevelUpUI.SetActive(true);
            xp = 10;
        }
        
    }

    public void CloseUpgradeWindow(){
        LevelUpUI.SetActive(false);
        //Debug.Log("upgrade the skills");
        //Debug.Log("find the bullet  " + bulletPrefab.name);
        
    }

    public void ChoosePlayer(){
        if(level % 3 == 0){
            index = level / 3;
            players[index].SetActive(true);
            level = 0;
        }
    }


    public void ModifyBulletSpawnerProperties()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.fireRate += newFireRate;
            bulletSpawner.burstCount += newBurstCount;
            bulletSpawner.parallelCount += newParallelCount;
        }
    }

    public void ModifyBulletProperties()
    {
        if (bulletPrefab != null)
        {
            //Debug.Log("find the bullet" + bulletPrefab.name);
            bulletPrefab.damage = newDamage;
            bulletPrefab.splitCount += newSplitCount;
        }
        else{
            //Debug.LogError("do not find the bullet");
        }
    }

}