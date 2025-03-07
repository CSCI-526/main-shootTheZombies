using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawner : MonoBehaviour
{
    public string buttonTextA = "a";
    public string buttonTextB = "bb";
    public string buttonTextC = "ccc";
    public TowerSpawner towerSpawner;
    public PlayerManagerScript playerManager;

    private GameObject buttonA;
    private GameObject buttonB;
    private GameObject buttonC;
    private GameObject canvas;

    private string chooseText;

    private TowerBase selectedTower;
    private bool canSelectTower = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateCanvas();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void InitializeButtons(string buttonTextA, string buttonTextB, string buttonTextC)
    {
        // If buttons already exist, don't recreate them
        if (buttonA == null)
            buttonA = CreateButton(buttonTextA, new Vector2(-200, -200));

        if (buttonB == null)
            buttonB = CreateButton(buttonTextB, new Vector2(0, -200));

        if (buttonC == null)
            buttonC = CreateButton(buttonTextC, new Vector2(200, -200)); // Avoid same position as buttonB
    
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
        }else
    {
        Canvas c = canvas.GetComponent<Canvas>();
        c.sortingOrder = 100; // Set a high sorting order to ensure the canvas is in front
    }
        
    }

 

    private GameObject CreateButton(string buttonText, Vector2 anchoredPosition)
    {
        GameObject buttonObj = new GameObject("Button_" +buttonText);
        buttonObj.transform.SetParent(canvas.transform,false); // Attach to Canvas

        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(160, 50); // Button size
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

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.sizeDelta = rectTransform.sizeDelta;
        textRect.anchoredPosition = Vector2.zero;
        Debug.Log("Button " + buttonText + " created and click listener added.");
        btn.onClick.AddListener(() => OnButtonClick(buttonText));

        return buttonObj;
    }

    private void OnButtonClick(string buttonText)
    {
        Debug.Log("Button " + buttonText + " clicked");
        Destroy(buttonA, 0.2f);
        Destroy(buttonB, 0.2f);
        Destroy(buttonC, 0.2f);
        UpdateTowerAttribute(buttonText);
        
    }

    //    // 1 : Increase Tower HP
    // 2 : Boost Tower Fire Rate
    // 3 : Speed Up Tower Bullets
    // 4 : Increase Tower Damage
    // 5 : Increase Tower Number

    private void UpdateTowerAttribute(string buttonText)
    {
         
        

        switch (buttonText)
        {
            case "Increase Tower Number":
                towerSpawner.incMaxTowerNumber();
                break; 
            case "Upgrade The Player Damage":
                playerManager.ModifyBulletSpawnerProperties();
                playerManager.CloseUpgradeWindow();
                break;
            default:
                canSelectTower = true;
                chooseText = buttonText;
                break;
        }
    }

        public void SelectTower(TowerBase tower)
    {
        if(!canSelectTower)
        {
            return;
        }
        selectedTower = tower;
        Debug.Log("Selected Tower: " + selectedTower);
        canSelectTower = false;
        UpdateTowerAttribute();
    }

        //    // 1 : Increase Tower HP
    // 2 : Boost Tower Fire Rate
    // 3 : Speed Up Tower Bullets
    // 4 : Increase Tower Damage
    // 5 : Increase Tower Number
    private void UpdateTowerAttribute()
    {
        if (selectedTower != null)
        {
            switch (chooseText)
            {
                case "Increase Tower HP":
                    selectedTower.hp += 10;
                    Debug.Log("Tower HP increased by 10. New HP: " + selectedTower.hp);
                    break;
                case "Boost Tower Fire Rate":
                    selectedTower.fireRate += 10;
                    Debug.Log("Tower fireRate increased by 10. New fireRate: " + selectedTower.fireRate);
                    break;

                case "Speed Up Tower Bullets":
                    selectedTower.bulletSpeed += 10;
                    Debug.Log("Tower bulletSpeed increased by 10. New bulletSpeed: " + selectedTower.bulletSpeed);
                    break;  
                case "Increase Tower Damage":
                    selectedTower.bulletDamage += 10;
                    Debug.Log("Tower bulletDamage increased by 10. New bulletDamage: " + selectedTower.bulletDamage);
                    break;  

                default:
                    Debug.LogWarning("Unknown button text: " + chooseText);
                    break;
            }
            // selectedTower.hp += 10;
            // Debug.Log("Tower HP increased by 10. New HP: " + selectedTower.hp);
        }
    }
}
