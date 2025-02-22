using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    void Start()
    {
        hintText.text = "Welcome! The shooter will shoot to the nearest zombie! Click the zombie you hate most to shoot as a priority!";
        CancelInvoke(nameof(HideHint)); 
        Invoke(nameof(HideHint), 10f); 
    }

    void HideHint(){
        hintText.text= "";
    }

}
