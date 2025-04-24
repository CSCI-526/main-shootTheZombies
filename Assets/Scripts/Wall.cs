using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.Networking;

public class Wall : MonoBehaviour
{
    // public TextMeshProUGUI hintText;
    public Image hintImage;
    public int health = 1000;  // 城墙初始血量
    public TMP_Text healthText;
    public Player _player;
    public TMP_Text survivalTime;
    private bool _hasGameEnded;

    void Start()
    {
        _hasGameEnded = false;
        _player = FindObjectOfType<Player>();
        // HideHint();
        hintImage.gameObject.SetActive(false);
        survivalTime.gameObject.SetActive(true);
    }
    void Update()
    {
        if (_hasGameEnded) return;
        healthText.text = "HP " + health;
        survivalTime.text =  $"You have survived for { _player.timer.ToString("F1") } seconds.";


        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        healthText.transform.position = screenPos;
        survivalTime.transform.position = screenPos + new Vector2(300, 50);

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

        
        // Debug.Log("----------------------");
        Debug.Log( _player.timer);
        StartCoroutine(Post());

        hintImage.gameObject.SetActive(true);
        Time.timeScale = 0;  
        _hasGameEnded = true;
    }

    private IEnumerator Post()
    {
        String URL = "https://docs.google.com/forms/d/e/1FAIpQLSdPZLi5PaXdtUP3Xnq6N4CKNW36kKlQinm560KTlYoery49_w/formResponse";
        WWWForm form = new WWWForm();
        form.AddField("entry.1032413562", _player.timer.ToString("F1"));
        form.AddField("entry.765727886", "Default User");

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log("-----------surival time upload error ------------");
                Debug.Log(www.error);
            } 
            // else
            // {
            //     Debug.Log("-----------surival time upload successfull");
            //     // Debug.Log("form upload complete");
            // }
        }
    }

    // void HideHint(){
    //     hintText.text = "";
    // }
}
