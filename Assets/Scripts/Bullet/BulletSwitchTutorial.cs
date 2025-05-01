using UnityEngine;
using TMPro;
using System.Collections;

public class BulletSwitchTutorial : MonoBehaviour
{
    public TextMeshProUGUI promptText;

    public RectTransform redBox;
    public RectTransform greenBox;
    public RectTransform blueBox;
    public float normalScale = 1f;
    public float highlightedScale = 1.1f;

    // private SpriteRenderer dummyRenderer;

    private bool redPressed = false;
    private bool greenPressed = false;
    private bool bluePressed = false;

    private bool initialHintCleared = false;
    private bool completionHintShown = false;
    private bool completionHintCleared = false;
    private bool completionHintReadyToClear = false;

    void Start()
    {
        promptText.text = "Press Q, W, or E to select Red, Blue, or Green bullets.\n\nCurrent bullet color is shown in the box on the right.";
        // dummyRenderer = dummyTarget.GetComponent<SpriteRenderer>();
        // dummyRenderer.color = Color.gray;
        SetAllBoxes(normalScale);
    }

    void Update()
    {
        bool q = Input.GetKeyDown(KeyCode.Q);
        bool w = Input.GetKeyDown(KeyCode.W);
        bool e = Input.GetKeyDown(KeyCode.E);

        if (!initialHintCleared && (q || w || e))
        {
            promptText.text = "";
            initialHintCleared = true;
        }

        if (q)
        {
            redPressed = true;
            // dummyRenderer.color = Color.red;
            Highlight(redBox);
        }
        else if (w)
        {
            greenPressed = true;
            // dummyRenderer.color = Color.green;
            Highlight(greenBox);
        }
        else if (e)
        {
            bluePressed = true;
            // dummyRenderer.color = Color.blue;
            Highlight(blueBox);
        }

        if (redPressed && greenPressed && bluePressed && !completionHintShown)
        {
            completionHintShown = true;
            StartCoroutine(ShowCompletionHints());
        }

        if (completionHintShown && !completionHintCleared && completionHintReadyToClear && (q || w || e || Input.GetMouseButtonDown(0)))
        {
            promptText.text = "";
            completionHintCleared = true;
            BulletTutorialManager.Instance.AdvanceStage();
        }
    }

    System.Collections.IEnumerator ShowCompletionHints()
    {
        promptText.text = "Good! You've learned to switch bullets.";
        yield return new WaitForSeconds(1.5f);

        promptText.text = "Left-click to proceed to the next step.";
        yield return new WaitForSeconds(1f);

        completionHintReadyToClear = true;
    }

    private void SetAllBoxes(float scale)
    {
        redBox.localScale   = Vector3.one * scale;
        greenBox.localScale = Vector3.one * scale;
        blueBox.localScale  = Vector3.one * scale;
    }

    private void Highlight(RectTransform boxToHighlight)
    {
        SetAllBoxes(normalScale * 0.95f);
        boxToHighlight.localScale = Vector3.one * highlightedScale;
    }

}
