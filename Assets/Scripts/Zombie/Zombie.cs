using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Zombie : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public int hp;
    public Color color;

    public bool isTutorialTarget = false;
    public AimingShootingTutorial tutorialRef;

    protected virtual void Start()
    {
        hp = 100;
    }

    public virtual void TakeDamage(int damageAmount, Color bulletColor)
    {   //Debug.Log("Zombie color: " + color + ", Bullet color: " + bulletColor);
        if (bulletColor != color && bulletColor != Color.black) return;
        hp -= damageAmount;

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
                    tutorialRef.OnZombieKilled();

                Destroy(gameObject);
                return;
            }
            //Debug.Log("Base Zombie Died: " + gameObject.name);
            Player player = GameObject.Find("Testplayer").GetComponent<Player>();
            Destroy(gameObject);
            if (SceneManager.GetActiveScene().name == "TutorialLevel"){
                player.GainExp(100);
             }else{
                player.GainExp(20);
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