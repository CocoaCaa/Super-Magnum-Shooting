using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;

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
}
