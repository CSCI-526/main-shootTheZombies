using System;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    public int hp;
    public Color color;

    protected virtual void Start()
    {
        hp = 100;
    }

    public virtual void TakeDamage(int damageAmount, Color bulletColor)
    {   //Debug.Log("Zombie color: " + color + ", Bullet color: " + bulletColor);
        if (bulletColor != color && bulletColor != Color.black) return;
        hp -= damageAmount;
        GameObject canvasObj = GameObject.Find("Damage");
        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damageAmount);

        if (hp <= 0)
        {
            //Debug.Log("Base Zombie Died: " + gameObject.name);
            Player player = GameObject.Find("Testplayer").GetComponent<Player>();
            Destroy(gameObject);
            if (player.playerLevel <= 6)
            {
                player.GainExp(20);
            }

            //AnalyticsCommon.timeZombieKilled = DateTime.Now.Ticks

            long timeZombieKilled = DateTime.Now.Ticks;

            // Call the singleton to send data
            if (SendToGoogle.Instance != null)
            {
                SendToGoogle.Instance.Send(timeZombieKilled);
            }
            else
            {
                Debug.LogError("SendToGoogle instance not found!");
            }
        }
    }

}