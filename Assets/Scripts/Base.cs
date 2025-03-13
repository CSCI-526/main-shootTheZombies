using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    void Start()
    {
        // Position the hintText at the top right
        RectTransform rectTransform = hintText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);
        rectTransform.anchoredPosition = new Vector2(-10, -10); // Adjust the offset as needed

        // Set the font size
        hintText.fontSize = 24; // Adjust the font size as needed

        // Set the width of the text field to be shorter
        rectTransform.sizeDelta = new Vector2(200, rectTransform.sizeDelta.y); // Adjust the width as needed

        hintText.text = "Welcome! Click the zombie to shoot!";
        CancelInvoke(nameof(ClickHint)); 
        Invoke(nameof(ClickHint), 10f); 
        Invoke(nameof(PlaceTowerHint), 20f);
        Invoke(nameof(ChangeTowerHint), 30f);                                         
    }

    void ClickHint(){
        hintText.text = "Click on a blank area to lock the shooting angle";
    }
    void PlaceTowerHint(){
        hintText.text = "Q/E to place the tower!";
    }
    void ChangeTowerHint(){
        hintText.text = "Z to change the tower!";
    }

}
