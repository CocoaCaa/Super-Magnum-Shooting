using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Canvas canvas;

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            Input.mousePosition,
            canvas.worldCamera,
            out pos
        );
        transform.position = canvas.transform.TransformPoint(pos);
    }

    public void DoFireAnimation()
    {
        GetComponent<Animator>().Play("CrosshairFireUI");
    }
}
