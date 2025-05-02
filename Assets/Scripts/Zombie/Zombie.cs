using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{

    [Header("Stats")]
    public int hp = 100;

    [Header("UI")]
    public Image healthFill; 

    protected int maxHp;


    public GameObject damagePopupPrefab;
    public Color color;
    
    protected const float xpDiminishingFactor = 0.2f;

    public bool isTutorialTarget = false;
    public AimingShootingTutorial tutorialRef;

    protected virtual void Start()
    {
        maxHp = hp;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthFill != null && maxHp > 0)
            healthFill.fillAmount = (float)hp / maxHp;
    }

    public virtual void TakeDamage(int damageAmount, Color bulletColor)
    {   //Debug.Log("Zombie color: " + color + ", Bullet color: " + bulletColor);
        if (bulletColor != color && bulletColor != Color.black)
        {
            // if (isTutorialTarget)
            // {
                var missCanvas = GameObject.Find("Damage");
                if (missCanvas != null)
                {
                    var canvastsfrm = missCanvas.transform;
                    var missPopup = Instantiate(
                        damagePopupPrefab,
                        transform.position + Vector3.up * 2f,
                        Quaternion.identity,
                        canvastsfrm
                    );
                    var txt = missPopup.GetComponentInChildren<TextMeshProUGUI>();
                    if (txt != null)
                    {
                        txt.text  = "Miss";
                        txt.color = Color.white;
                    }
                    Destroy(missPopup, 1f);
                }
            // }
            return;
        }

        // if (bulletColor != color && bulletColor != Color.black) return;
        hp -= damageAmount;


        // hp -= damageAmount;
        hp = Mathf.Max(0, hp - damageAmount);
        UpdateHealthBar();

        SendAccuracy.bulletsHit += 1;

        GameObject canvasObj = GameObject.Find("Damage");
        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damageAmount);

        if (hp <= 0)
        {

            if (isTutorialTarget)
            {
                if (tutorialRef != null)
                    tutorialRef.OnZombieKilled(this);

                Destroy(gameObject);
                return;
            }
            //Debug.Log("Base Zombie Died: " + gameObject.name);
            if (healthFill != null)
                healthFill.transform.parent.gameObject.SetActive(false);

            Player player = GameObject.Find("Testplayer").GetComponent<Player>();
            Destroy(gameObject);
            if (SceneManager.GetActiveScene().name == "TutorialLevel"){
                player.GainExp(50);
             }else{
                int baseXp = (SceneManager.GetActiveScene().name == "TutorialLevel") ? 50 : 20;
                int lvl = player.playerLevel;
                int reward = Mathf.CeilToInt(baseXp / (1f + (lvl - 1) * xpDiminishingFactor));
                player.GainExp(reward);
            }


            //AnalyticsCommon.timeZombieKilled = DateTime.Now.Ticks

            long timeZombieKilled = DateTime.Now.Ticks;

            // Call the singleton to send data
            if (SendZombieKillRate.Instance != null)
            {
                SendZombieKillRate.Instance.Send(timeZombieKilled);
            }
            else
            {
                Debug.LogError("SendZombieKillRate instance not found!");
            }
        }
    }
}