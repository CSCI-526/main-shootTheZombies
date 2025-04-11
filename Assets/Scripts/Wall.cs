using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    // public TextMeshProUGUI hintText;
    public Image hintImage;
    public int health = 1000;  // 城墙初始血量
    public TMP_Text healthText;

    void Start()
    {
        // HideHint();
        hintImage.gameObject.SetActive(false);
    }
    void Update()
    {
        healthText.text = "HP " + health;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        healthText.transform.position = screenPos;

        if (health <= 0)
        {   
            healthText.text = "HP " + 0f;
            GameOver();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void GameOver()
    {
        // 游戏结束逻辑
        // //Debug.Log("Game Over");
        // hintText.text = "The zombies ate your brains!";
        // CancelInvoke(nameof(HideHint)); 
        // Invoke(nameof(HideHint), 1f); 
        hintImage.gameObject.SetActive(true);
        Time.timeScale = 0;  
    }

    // void HideHint(){
    //     hintText.text = "";
    // }
}
