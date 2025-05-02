using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BulletSwitchTutorial : MonoBehaviour
{
    public RectTransform redBox;
    public RectTransform greenBox;
    public RectTransform blueBox;
    public Outline redOutline;
    public Outline greenOutline;
    public Outline blueOutline;
    public float normalScale = 1f;
    public float highlightedScale = 1.3f;

    public SpriteRenderer redHintSprite;
    public SpriteRenderer greenHintSprite;
    public SpriteRenderer blueHintSprite;

    private bool redPressed = false;
    private bool greenPressed = false;
    private bool bluePressed = false;

    private bool initialHintCleared = false;
    private bool completionHintShown = false;
    private bool completionHintCleared = false;
    private bool completionHintReadyToClear = false;

    void Start()
    {
        if (redHintSprite   != null) redHintSprite.enabled   = true;
        if (greenHintSprite != null) greenHintSprite.enabled = true;
        if (blueHintSprite  != null) blueHintSprite.enabled  = true;
        SetAllBoxes(normalScale);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Highlight(redBox, redOutline);
            if (!redPressed)
            {
                redPressed = true;
                if (redHintSprite != null) redHintSprite.enabled = false;
                TryAdvance();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Highlight(greenBox, greenOutline);
            if (!greenPressed)
            {
                greenPressed = true;
                if (greenHintSprite != null) greenHintSprite.enabled = false;
                TryAdvance();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Highlight(blueBox, blueOutline);
            if (!bluePressed)
            {
                bluePressed = true;
                if (blueHintSprite != null) blueHintSprite.enabled = false;
                TryAdvance();
            }
        }
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

    private void TryAdvance()
    {
        if (redPressed && greenPressed && bluePressed)
        {
            BulletTutorialManager.Instance.AdvanceStage();
        }
    }

}
