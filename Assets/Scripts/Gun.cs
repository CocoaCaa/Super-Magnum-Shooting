using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [System.Serializable]
    public class OnHitEvent : UnityEvent<int>
    {
    }

    public OnHitEvent OnHit;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && Input.GetMouseButtonDown(0))
        {
            var score = hit.collider.GetComponent<Target>().score;
            OnHit.Invoke(score);
        }

        UpdateRotation();
    }

    private void UpdateRotation()
    {
        var mousePos = Input.mousePosition;
        var percX = Mathf.Clamp(mousePos.x / Screen.width, 0.0f, 0.5f);
        var percY = 1 - Mathf.Clamp(mousePos.y / Screen.height, 0.0f, 0.5f);

        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, (-percX * 20) + (percY * 12));
    }
}
