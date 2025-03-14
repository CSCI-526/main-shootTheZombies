using UnityEngine;
using UnityEngine.UI;


public class ButtonSpawner : MonoBehaviour
{
    public PlayerManagerScript playerManager;
    public string buttonTextA = "a";
    public string buttonTextB = "bb";
    public string buttonTextC = "ccc";
    public TowerSpawner towerSpawner;

    private GameObject buttonA;
    private GameObject buttonB;
    private GameObject buttonC;
    private GameObject canvas;
    private GameObject hintTextObj; // Add this line to store the hint text object

    private GameObject hintClickTower;
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

        
         // Add hint text
        hintTextObj = CreateText("Level Up!", new Vector2(0, 0));
    
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
        hintTextText.fontSize = 24;
        hintTextText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        hintTextText.alignment = TextAnchor.MiddleCenter;
        hintTextText.color = Color.red;

        return hintTextObj;
    }
    private void OnButtonClick(string buttonText)
    {
        Debug.Log("Button " + buttonText + " clicked");
        Destroy(buttonA, 0.2f);
        Destroy(buttonB, 0.2f);
        Destroy(buttonC, 0.2f);
        Destroy(hintTextObj, 0.2f); // Destroy the hint text
        UpdateTowerAttribute(buttonText);
        
    }

    //    // 1 : Increase Tower HP
    // 2 : Boost Tower Fire Rate
    // 3 : Speed Up Tower Bullets
    // 4 : Increase Tower Damage
    // 5 : Increase Tower Number

    private void UpdateTowerAttribute(string buttonText)
    {
         canSelectTower = true;
        

        switch (buttonText)
        {
            case "Tower Number":
                 towerSpawner.incMaxTowerNumber();
                 hintClickTower = CreateText("Press Q/E to place the tower!", new Vector2(0, 0));
                 Destroy(hintClickTower, 2f);
                 break;
                
                break;
            case "Player Damage":
                playerManager.ModifyBulletSpawnerProperties();
                playerManager.CloseUpgradeWindow();
                break;
            // case "Boost Tower Fire Rate":
            //     towerSpawner.UpdateAttributeB();
            //     break;
            // case "Speed Up Tower Bullets":
            //     towerSpawner.UpdateAttributeC();
            //     break;
            // case "Increase Tower Damage":
            //     towerSpawner.UpdateAttributeD();
            //     break;  
   
            
            default:
                hintClickTower = CreateText("Click the tower to upgrade!", new Vector2(0, 0));
                chooseText = buttonText;
                //Debug.LogWarning("Unknown button text: " + buttonText);
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
        // //Debug.Log("Selected Tower: " + selectedTower);
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
            if (hintClickTower != null)
            {
                //change the hint text
                hintClickTower.GetComponent<Text>().text ="upgraded!";
                
                // hintClickTower.= "upgraded!";
                Destroy(hintClickTower, 2f);

            }


            switch (chooseText)
            {
                case "Tower HP":
                    selectedTower.hp += 10;
                    // //Debug.Log("Tower HP increased by 10. New HP: " + selectedTower.hp);
                    break;
                case "Tower Fire Rate":
                    selectedTower.fireRate += 10;
                    // //Debug.Log("Tower fireRate increased by 10. New fireRate: " + selectedTower.fireRate);
                    break;

                case "Tower Bullets":
                    selectedTower.bulletSpeed += 10;
                    // //Debug.Log("Tower bulletSpeed increased by 10. New bulletSpeed: " + selectedTower.bulletSpeed);
                    break;  
                case "Tower Damage":
                    selectedTower.bulletDamage += 10;
                    // //Debug.Log("Tower bulletDamage increåased by 10. New bulletDamage: " + selectedTower.bulletDamage);
                    break;  

                
                default:
                    //Debug.LogWarning("Unknown button text: " + chooseText);
                    break;
            }
            // selectedTower.hp += 10;
            // //Debug.Log("Tower HP increased by 10. New HP: " + selectedTower.hp);
        }
    }
}
