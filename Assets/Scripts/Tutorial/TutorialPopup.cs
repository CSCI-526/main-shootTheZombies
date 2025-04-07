using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TutorialPopup : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;
    public float displayDuration = 3f;

    private void Start()
    {
        popupPanel.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayPopup(message));
    }

    private IEnumerator DisplayPopup(string message)
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        popupPanel.SetActive(false);
    }
}