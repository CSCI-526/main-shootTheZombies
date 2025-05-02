using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManagerScript : MonoBehaviour
{
    public GameObject popupTextObject;
    private TMP_Text popupText;
    public float displayTime = 1.5f;

    public Player player;
    public Wall wall;
    public Image expbar;
    public Image HPbar;
    public float maxXp = 100f;
    public float maxHp = 100f;

    public int level = 1;
    public float xp = 0f;

    public GameObject LevelUpUI;
    public GameObject[] players;
    private int index;

    public BulletSpawner bulletSpawner;
    public Bullet bulletPrefab;

    public float newFireRate = 1f;
    public int newBurstCount = 1;
    public int newParallelCount = 1;

    public int newDamage = 20;
    public int newSplitCount = 2;

    void Start()
    {
        popupText = popupTextObject.GetComponent<TMP_Text>();
        popupTextObject.SetActive(true);
        popupText.text = "Exp:";
    }

    void Update()
    {
        expbar.fillAmount = Mathf.Clamp(player.exp / maxXp, 0, 1);
        HPbar.fillAmount = Mathf.Clamp(wall.health / maxHp, 0, 1);

        ChoosePlayer();
        LevelUp();
    }

    public void LevelUp()
    {
        if (player.exp >= maxXp)
        {
            // LevelUpUI.SetActive(true);
            player.exp = 0;
            level += 1;
            ShowPopup("Level Up!");
        }
    }

    public void CloseUpgradeWindow()
    {
        LevelUpUI.SetActive(false);
    }

    public void ChoosePlayer()
    {
        if (players != null && players.Length > index && level % 3 == 0)
        {
            index = player.playerLevel / 3;
            if (index >= 0 && index < players.Length)
                // players[index].SetActive(true);
            level = 1;
        }
    }

    public void ModifyBulletSpawnerProperties()
    {
        if (bulletSpawner != null)
        {
            bulletSpawner.burstCount += newBurstCount;
            bulletSpawner.parallelCount += newParallelCount;
        }
    }

    public void ModifyBulletProperties()
    {
        if (bulletPrefab != null)
        {
            bulletPrefab.damage = newDamage;
            bulletPrefab.splitCount += newSplitCount;
        }
        else
        {
            Debug.LogError("do not find the bullet");
        }
    }

    public void ShowPopup(string message)
    {
        popupText.text = message;
        popupText.enabled = true;
        CancelInvoke(nameof(ResetPopup));
        Invoke(nameof(ResetPopup), displayTime);
    }

    private void ResetPopup()
    {
        popupText.text = "Exp:";
        popupText.enabled = true;
    }
}