using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BulletSwitchTutorial : MonoBehaviour
{
    public TextMeshProUGUI promptText;

    public RectTransform redBox;
    public RectTransform greenBox;
    public RectTransform blueBox;
    public Outline redOutline;
    public Outline greenOutline;
    public Outline blueOutline;
    public float normalScale = 1f;
    public float highlightedScale = 1.3f;

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
        promptText.text = "Press Q, W, E to select bullets for attacking zombies.";
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
            Highlight(redBox, redOutline);
        }
        else if (w)
        {
            greenPressed = true;
            // dummyRenderer.color = Color.green;
            Highlight(greenBox, greenOutline);
        }
        else if (e)
        {
            bluePressed = true;
            // dummyRenderer.color = Color.blue;
            Highlight(blueBox, blueOutline);
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
        promptText.text = "Good job!";
        yield return new WaitForSeconds(1f);

        promptText.text = "Left-click to proceed to the next step.";
        yield return new WaitForSeconds(1f);

        completionHintReadyToClear = true;
    }

    private void SetAllBoxes(float scale)
    {
        redBox.localScale   = Vector3.one * scale;
        greenBox.localScale = Vector3.one * scale;
        blueBox.localScale  = Vector3.one * scale;
        redOutline.enabled = false;
        greenOutline.enabled = false;
        blueOutline.enabled = false;
    }

    private void Highlight(RectTransform boxToHighlight, Outline outline)
    {
        SetAllBoxes(normalScale * 0.8f);
        boxToHighlight.localScale = Vector3.one * highlightedScale;
        redOutline.enabled   = false;
        greenOutline.enabled = false;
        blueOutline.enabled  = false;

        outline.effectColor    = outline == redOutline   ? Color.red   :
                                 outline == greenOutline ? Color.green : Color.blue;

        outline.enabled = true;
    }

}
