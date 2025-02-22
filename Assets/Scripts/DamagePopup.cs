using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private float moveSpeed = 1f;     
    private float disappearSpeed = 1f;

    public void Setup(int damageAmount)
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();

        textMesh.text = damageAmount.ToString();
        textMesh.color = Color.red;  
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = startPos + new Vector3(0, elapsedTime * moveSpeed, 0);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1 - elapsedTime);
            elapsedTime += Time.deltaTime * disappearSpeed;
            yield return null;
        }

        Destroy(gameObject);
    }
}
