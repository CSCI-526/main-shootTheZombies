using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerButtonSpawner : MonoBehaviour
{
    public PlayerManagerScript playerManager;
    private string buttonTextA = "Normal Tower";
    private string buttonTextB = "Flame Tower";
    private string buttonTextC = "Defense Tower";
    public TowerSpawner towerSpawner;
    private GameObject TbuttonA;
    private GameObject TbuttonB;
    private GameObject TbuttonC;
    private GameObject canvas;
    private GameObject hintTextObj; // Add this line to store the hint text object
    private Player player;

    private GameObject hintClickTower;
    private string chooseText;

    private TowerBase selectedTower;
    private bool canSelectTower = false;

    private bool hasShownHint = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        towerSpawner = FindObjectOfType<TowerSpawner>();
        player = FindObjectOfType<Player>();
        CreateCanvas();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeButtons()
    {
        // If buttons already exist, don't recreate them
        if (TbuttonA == null && buttonTextA != "")
            TbuttonA = CreateButton(buttonTextA, new Vector2(-250, -200));

        if (TbuttonB == null && buttonTextB != "")
            TbuttonB = CreateButton(buttonTextB, new Vector2(0, -200));


        if (TbuttonC == null && buttonTextC != "")
            TbuttonC = CreateButton(buttonTextC, new Vector2(250, -200)); // Avoid same position as buttonB


        // Add hint text
        hintTextObj = CreateText("Add a Tower Now!", new Vector2(0, 0));

    }



    private void CreateCanvas()
    {
        // Check if a Canvas already exists
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
            c.sortingOrder = 100; // Set a high sorting order to ensure the canvas is in front
        }

        CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
        if (canvasScaler != null)
        {
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080); // Reference resolution
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // Adjust to balance width and height scaling
        }
    }



    private GameObject CreateButton(string buttonText, Vector2 anchoredPosition)
    {
        GameObject buttonObj = new GameObject("Button_" + buttonText);
        buttonObj.transform.SetParent(canvas.transform, false); // Attach to Canvas

        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Center anchor
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f); // Center anchor
        rectTransform.pivot = new Vector2(0.5f, 0.5f); // Center pivot
        rectTransform.sizeDelta = new Vector2(200, 50); // Button size
        rectTransform.anchoredPosition = anchoredPosition; // Button position

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
        text.fontSize = 20; // Enlarged text size

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.sizeDelta = rectTransform.sizeDelta;
        textRect.anchoredPosition = Vector2.zero;
        // //Debug.Log("Button " + buttonText + " created and click listener added.");
        btn.onClick.AddListener(() => OnButtonClick(buttonText));

        return buttonObj;
    }

    private GameObject CreateText(string hintText, Vector2 anchoredPosition)
    {
        GameObject hintTextObj = new GameObject("HintText");
        hintTextObj.transform.SetParent(canvas.transform, false);

        RectTransform hintTextRect = hintTextObj.AddComponent<RectTransform>();
        hintTextRect.sizeDelta = new Vector2(480, 50); // Hint text size
        hintTextRect.anchoredPosition = anchoredPosition; // Hint text position

        Text hintTextText = hintTextObj.AddComponent<Text>();
        hintTextText.text = hintText;
        hintTextText.fontSize = 40;
        hintTextText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        hintTextText.alignment = TextAnchor.MiddleCenter;
        hintTextText.color = Color.red;

        return hintTextObj;
    }
    private void OnButtonClick(string buttonText)
    {
        Time.timeScale = 1;
        // Debug.Log("Button " + buttonText + " clicked");
        if (TbuttonA != null)
        {
            Destroy(TbuttonA, 0.2f);

        }
        if (TbuttonB != null)
        {
            Destroy(TbuttonB, 0.2f);

        }
        if (TbuttonC != null)
        {
            Destroy(TbuttonC, 0.2f);

        }
        Destroy(hintTextObj, 0.2f); // Destroy the hint text
        Invoke(nameof(ShowHintText), 1f);
        player.ResumeGame();

        long timeTowerSelection = DateTime.Now.Ticks;
        string typeTowerSelection = "";

        if (buttonText==buttonTextA){
            towerSpawner.AddTower(0);
            typeTowerSelection = "normal";
        }
        else if(buttonText == buttonTextB){
            towerSpawner.AddTower(1);
            typeTowerSelection = "flame";
        }
        else if(buttonText == buttonTextC){
            towerSpawner.AddTower(2);
        }

        // Call the singleton to send data
        if (SendTowerSelection.Instance != null)
        {
            SendTowerSelection.Instance.Send(timeTowerSelection, typeTowerSelection);
        }
        else
        {
            Debug.LogError("SendTowerSelection instance not found!");
        }

    }

    void ShowHintText()
    {
        if (hasShownHint) return;
        hasShownHint = true;
        hintClickTower = CreateText("Click to deploy a tower", new Vector2(0, 0));
        // Destroy after 2 seconds if it exists
        if (hintClickTower != null)
        {
            Destroy(hintClickTower, 2f);
        }
    }

    public void destoryHint(){

        // Debug.Log("destory");
        Destroy(hintClickTower);

    }

}
