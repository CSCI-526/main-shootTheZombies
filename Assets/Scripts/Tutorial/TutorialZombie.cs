using UnityEngine;

public class TutorialZombie : Zombie
{
    private TutorialManager tutorialManager;
    // public GameObject damagePopupPrefab;
    // public int hp;
    // public Color color;

    public void SetTutorialManager(TutorialManager manager)
    {
        tutorialManager = manager;
    }

    protected virtual void Start()
    {
        hp = 100;
    }

    public override void TakeDamage(int damage, Color bulletColor)
    {
        if (bulletColor != color && bulletColor != Color.black) return;
        hp -= damage;
        GameObject canvasObj = GameObject.Find("Damage");
        Transform canvas = canvasObj.transform;

        GameObject popup = Instantiate(damagePopupPrefab, transform.position + Vector3.up, Quaternion.identity, canvas);
        popup.GetComponent<DamagePopup>().Setup(-damage);

        if (hp <= 0)
        {
            Player player = GameObject.Find("Testplayer").GetComponent<Player>();
            tutorialManager?.OnZombieKilled();
            Destroy(gameObject);
            if (player.playerLevel <= 6)
            {
                player.GainExp(20);
            }
        }
    }

    private void OnDestroy()
    {
        TutorialManager tutorial = FindObjectOfType<TutorialManager>();
        if (tutorial != null)
        {
            tutorial.OnZombieKilled();
        }
    }
}