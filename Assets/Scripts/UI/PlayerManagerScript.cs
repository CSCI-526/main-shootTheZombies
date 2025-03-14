using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManagerScript : MonoBehaviour
{   
    //intro of the zombies
    public GameObject popupTextObject;
    private TMP_Text popupText;
    public float displayTime = 10f;


    // get all the variable from the player class
    public Player player;
    //exp bar setting 
    public Image expbar;
    public float maxXp = 100f;

    //will change after we get the xp and level from others
    public int level = 1;
    public float xp = 0f;
    
    public GameObject LevelUpUI;
    public GameObject[] players; 
    private int index;

    public BulletSpawner bulletSpawner;
    public Bullet bulletPrefab;

    // BulletSpawner
    public float newFireRate = 1f;
    public int newBurstCount = 1;
    public int newParallelCount = 1;

    // Bullet
    public int newDamage = 20;
    public int newSplitCount = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupText = popupTextObject.GetComponent<TMP_Text>();
        popupTextObject.SetActive(false);
        // if (players != null && players.Length > 2 && players[2] != null)
        // {
        //     GameObject child = players[2].transform.Find("SimpleBullet")?.gameObject;
        //     if (child != null)
        //     {
        //         Debug.Log("找到子对象：" + child.name);
        //         Debug.Log("找到对象：" + players[2].name);
        //     }
        //     else
        //     {
        //         Debug.LogError("SimpleBullet not found in player " + players[2].name);
        //     }
        // }
        // else
        // {
        //     Debug.LogError("Player array is not properly initialized or player[2] is null.");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("exp == " + player.exp);

        //update the exp bar 
        expbar.fillAmount = Mathf.Clamp(player.exp / maxXp, 0, 1);


        // add more player
        ChoosePlayer();

        //show the UI of choosing skills
        LevelUp();
    }



    public void LevelUp(){
        if(player.exp == 100){
            LevelUpUI.SetActive(true);
            player.exp = 0;
            level += 1;
        }
        
    }

    public void CloseUpgradeWindow(){
        LevelUpUI.SetActive(false);
        Debug.Log("upgrade the skills");
        Debug.Log("find the bullet  " + bulletPrefab.name);
        
    }

    public void ChoosePlayer(){
        if(level % 3 == 0){
            index = player.playerLevel / 3;
            players[index].SetActive(true);
            level = 1;
        }
    }


    public void ModifyBulletSpawnerProperties()
    {
        if (bulletSpawner != null)
        {
            //bulletSpawner.fireRate += newFireRate;
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
            Debug.LogError("do not find the bullet");
        }
    }


    //text of the introduction of zombies
    public void ShowPopup(string message)
    {
        popupText.text = message;
        popupTextObject.SetActive(true);
        CancelInvoke(); // in case there's a previous Invoke running
        Invoke(nameof(HidePopup), displayTime);
    }

    private void HidePopup()
    {
        popupTextObject.SetActive(false);
    }


}