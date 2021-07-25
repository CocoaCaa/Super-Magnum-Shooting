using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetScoreText : MonoBehaviour
{
    private static readonly Color[] COLORS = new Color[] { Color.white, Color.yellow, Color.green, Color.cyan, Color.magenta };

    public TextMeshProUGUI text;
    private int currentColorIdx = 0;
    private float changeColorRate = 0.1f;
    private float lastChangeColorTime = 0.0f;

    public void SetToTarget(RectTransform rectTransformCanvas, int score, Vector3 hitPoint)
    {
        StartCoroutine(HandleSetToTarget(rectTransformCanvas, score, hitPoint));
    }

    private IEnumerator HandleSetToTarget(RectTransform rectTransformCanvas, int score, Vector3 hitPoint)
    {
        text.text = "+" + score;

        var uiOffset = new Vector2(rectTransformCanvas.sizeDelta.x / 2f, rectTransformCanvas.sizeDelta.y / 2f);
        var viewportPosition = Camera.main.WorldToViewportPoint(hitPoint);
        var worldPosition = new Vector2(
            viewportPosition.x * rectTransformCanvas.sizeDelta.x,
            viewportPosition.y * rectTransformCanvas.sizeDelta.y
        );
        GetComponent<RectTransform>().localPosition = worldPosition - uiOffset;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void Update()
    {
        if (Time.time > lastChangeColorTime + changeColorRate)
        {
            lastChangeColorTime = Time.time;
            text.faceColor = COLORS[currentColorIdx];
            currentColorIdx = (currentColorIdx + 1) % COLORS.Length;
        }
    }
}
