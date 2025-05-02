using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class AimingShootingTutorial : MonoBehaviour
{
    // public TextMeshProUGUI promptText;
    public GameObject meleeZombiePrefab;
    public GameObject rangedZombiePrefab;
    public GameObject explodingZombiePrefab;
    public GameObject damagePopupPrefab;
    public Texture2D tutorialCursor;
    public float    cursorAnimDuration = 2f;

    private BulletSpawner spawner; 
    private int targetsRemaining = 3;
    private bool tutorialStarted = false;
    private Dictionary<Zombie, GameObject> hintMap = new Dictionary<Zombie, GameObject>();

    void Awake()
    {
        spawner = FindObjectOfType<BulletSpawner>();
    }

    public void StartTutorial()
    {
        tutorialStarted = true;
        var canvas = FindObjectOfType<Canvas>().transform;
        var go = new GameObject("TutorialCursor", typeof(RawImage));
        go.transform.SetParent(canvas, false);

        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.6f);
        rt.pivot     = new Vector2(0.5f, 0.6f);
        rt.anchoredPosition = Vector2.zero;

        var ri = go.GetComponent<RawImage>();
        ri.texture = tutorialCursor;
        rt.sizeDelta = new Vector2(tutorialCursor.width, tutorialCursor.height);
        go.transform.localScale = Vector3.one;

        StartCoroutine(ScaleAndDestroy(go.transform, 1.5f, 2f, cursorAnimDuration));

        if (spawner != null)
            spawner.showGuideInTutorial = true;

        SpawnZombie(meleeZombiePrefab, new Vector3(-4f,6f,0f), "Q", Color.red);
        SpawnZombie(rangedZombiePrefab, new Vector3(0f,6f,0f), "W", Color.green);
        SpawnZombie(explodingZombiePrefab, new Vector3(4f,6f,0f), "E", Color.blue);
    }

    void SpawnZombie(GameObject prefab, Vector3 position, string key, Color color)
    {
        GameObject zombie = Instantiate(prefab, position, Quaternion.identity);
        var tz = zombie.GetComponentInChildren<Zombie>();
        if (tz == null) return;
        tz.hp = 30;
        tz.isTutorialTarget = true;
        tz.tutorialRef = this;

        GameObject canvasObj = GameObject.Find("Damage");
        Transform canvas = canvasObj.transform;
        Vector3 headPos = zombie.transform.position + Vector3.up * 2f;
        GameObject popup = Instantiate(damagePopupPrefab, headPos, Quaternion.identity, canvas);
        var textMesh = popup.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text  = key;
            textMesh.color = color;
            textMesh.fontSize = 50;
        }
        hintMap[tz] = popup;
    }

    public void OnZombieKilled(Zombie tz)
    {
        if (hintMap.TryGetValue(tz, out GameObject hint))
        {
            Destroy(hint);
            hintMap.Remove(tz);
        }

        targetsRemaining--;
        if (targetsRemaining <= 0)
        {
            spawner.showGuideInTutorial = false;
            BulletTutorialManager.Instance.AdvanceStage();
        }
    }

    private IEnumerator ClearPromptAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // promptText.text = "";
        // Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private IEnumerator ScaleAndDestroy(Transform t, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float s = Mathf.Lerp(from, to, elapsed / duration);
            t.localScale = Vector3.one * s;
            elapsed += Time.deltaTime;
            yield return null;
        }
        t.localScale = Vector3.one * to;
        Destroy(t.gameObject);
    }
}
