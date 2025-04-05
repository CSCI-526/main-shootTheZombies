using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    void Start()
    {
        // Position the hintText at the top right
         RectTransform rectTransform = hintText.GetComponent<RectTransform>();

        // Set the anchor to the center of the screen
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f); 
        rectTransform.anchoredPosition =  new Vector2(0, 200); // Adjust the offset as needed

        // Set the font size
        hintText.fontSize = 24; // Adjust the font size as needed

        // Set the width of the text field to be shorter
        rectTransform.sizeDelta = new Vector2(200, rectTransform.sizeDelta.y); // Adjust the width as needed

        hintText.text = "Click the zombie to shoot!";
        
        Invoke(nameof(ClickHint), 5f); 
        // Invoke(nameof(PlaceTowerHintLeft), 20f);
        // Invoke(nameof(PlaceTowerHintRight), 25f);
        // Invoke(nameof(PlaceTowerHintZ), 30f);
        // Invoke(nameof(ChangeTowerHint), 35f);    
        Invoke(nameof(CancelHint), 10f);
                                   
    }

    void ClickHint(){
        UpdateHint("Using the Q W Z to change the color to shoot different zombies", new Vector2(0, -100), 24, 200);
    }
    // void PlaceTowerHintLeft(){
    //     UpdateHint("Try Q for towers!", new Vector2(-200, 0), 24, 200);
    // }
    //  void PlaceTowerHintRight(){
    //     UpdateHint("Try E for towers!", new Vector2(200, 0), 24, 200);
    // }
    //  void PlaceTowerHintZ(){
    //     UpdateHint("Try Z to switch!", new Vector2(200, 0), 24, 200);
    // }
    // void ChangeTowerHint(){
    //     UpdateHint("Move the mouse and left click!", new Vector2(200, 0), 24, 200);
    // }
    void CancelHint(){
        // UpdateHint(" ", new Vector2(0, 0), 24, 200);
        hintText.text = "";
    }

    public void UpdateHint(string newText, Vector2 newPosition, float newFontSize, float newWidth)
    {
        hintText.text = newText;
        RectTransform rectTransform = hintText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = newPosition;
        hintText.fontSize = newFontSize;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
    }
}
