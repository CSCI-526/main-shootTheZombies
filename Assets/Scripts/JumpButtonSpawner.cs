using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class JumpButtonSpawner : MonoBehaviour
{
    public PlayerManagerScript playerManager;
    public GameObject hintTextObj;
    private string buttonTextA = "Menu";
    private string buttonTextC = "Regular Level";

    public TowerSpawner towerSpawner;
    private GameObject TbuttonA;
    private GameObject TbuttonB;
    private GameObject TbuttonC;
    private GameObject canvas;
    private Player player;

    private GameObject hintClickTower;
    private string chooseText;

    private TowerBase selectedTower;
    private bool canSelectTower = false;

    private bool hasShownHint = false;
    public Material hintMaterial;

    void Start()
    {
        towerSpawner = FindObjectOfType<TowerSpawner>();
        player = FindObjectOfType<Player>();
        CreateCanvas();
    }

    void Update()
    {
    }

    public void InitializeButtons()
    {
        if (TbuttonA == null && buttonTextA != "")
            TbuttonA = CreateButton(buttonTextA, new Vector2(-200, -200));

        if (TbuttonC == null && buttonTextC != "")
            TbuttonC = CreateButton(buttonTextC, new Vector2(200, -200));

        ShowHintText();
    }

    private void CreateCanvas()
    {
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            canvas = new GameObject("Canvas");
            Canvas c = canvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            c.sortingOrder = 100;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
        }
        else
        {
            Canvas c = canvas.GetComponent<Canvas>();
            c.sortingOrder = 100;
        }
    }

    private GameObject CreateButton(string buttonText, Vector2 anchoredPosition)
    {
        GameObject buttonObj = new GameObject("Button_" + buttonText);
        buttonObj.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(200, 50);
        rectTransform.anchoredPosition = anchoredPosition;

        Button btn = buttonObj.AddComponent<Button>();
        Image img = buttonObj.AddComponent<Image>();
        img.color = new Color(0.9f, 0.9f, 0.9f);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform);

        Text text = textObj.AddComponent<Text>();
        text.text = buttonText;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.fontSize = 24;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.sizeDelta = rectTransform.sizeDelta;
        textRect.anchoredPosition = Vector2.zero;

        btn.onClick.AddListener(() => OnButtonClick(buttonText));

        return buttonObj;
    }

    private GameObject CreateText(string message, Vector2 anchoredPosition)
    {
        GameObject go = new GameObject("HintText", typeof(RectTransform));
        go.transform.SetParent(canvas.transform, false);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(480, 50);
        rt.anchoredPosition = anchoredPosition;

        TextMeshProUGUI tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.text = message;
        tmp.fontSize = 40;
        tmp.alignment = TextAlignmentOptions.Center;
        if (hintMaterial != null)
            tmp.fontSharedMaterial = hintMaterial;

        return go;
    }

    private void OnButtonClick(string buttonText)
    {
        Time.timeScale = 1;
        if (TbuttonA != null)
            Destroy(TbuttonA, 0.2f);
        if (TbuttonB != null)
            Destroy(TbuttonB, 0.2f);
        if (TbuttonC != null)
            Destroy(TbuttonC, 0.2f);
        Destroy(hintTextObj, 0.2f);
        player.ResumeGame();

        if (buttonText == buttonTextA)
            SceneManager.LoadScene("MainPage");
        else if (buttonText == buttonTextC)
            SceneManager.LoadScene("RegularLevel");
        if (hintClickTower != null)
            Destroy(hintClickTower);
    }

    void ShowHintText()
    {
        if (hasShownHint) return;
        hasShownHint = true;
        hintTextObj = CreateText("Congratulations!", new Vector2(0, 0));
    }

    public void destoryHint()
    {
        Destroy(hintClickTower);
    }
}
