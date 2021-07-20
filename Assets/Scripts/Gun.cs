using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    [System.Serializable]
    public class OnHitEvent : UnityEvent<int>
    {
    }

    public OnHitEvent OnHit;
    public GameObject bulletHole;
    public float fireRate = 0.37f;
    public float reloadTime = 1.3f;
    public ReloadProgressBar uiReloadProgressBar;
    public TextMeshProUGUI uiCurrentCapacity;
    public int capacity = 6;
    public Crosshair uiCrosshair;
    private int currentCapacity = 0;
    private float lastFireTime = 0.0f;

    void Start()
    {
        currentCapacity = capacity;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentCapacity > 0)
        {
            HandleFire();
            StartCoroutine(HandleReload());
        }

        UpdateRotation();
    }

    private void HandleFire()
    {
        if (Time.time < lastFireTime + fireRate)
        {
            return;
        }

        lastFireTime = Time.time;

        currentCapacity--;
        uiCurrentCapacity.text = currentCapacity.ToString();

        GetComponent<Animator>().Play("GunFire");
        uiCrosshair.DoFireAnimation();
        var hitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(hitPoint, Vector2.zero);
        if (hit.collider != null)
        {
            var score = hit.collider.GetComponent<Target>().score;
            OnHit.Invoke(score);

            var newBulletHole = Instantiate(bulletHole);
            newBulletHole.transform.position = new Vector3(hitPoint.x, hitPoint.y, hit.transform.position.z);
            newBulletHole.transform.parent = hit.transform;
            newBulletHole.transform.localScale = hit.transform.localScale;

            var hitSpriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
            var newBulletHoleSpriteRenderer = newBulletHole.GetComponent<SpriteRenderer>();
            newBulletHoleSpriteRenderer.sortingOrder = hitSpriteRenderer.sortingOrder + 1;
        }
    }

    private IEnumerator HandleReload()
    {
        if (currentCapacity == 0)
        {
            uiReloadProgressBar.time = reloadTime;
            uiReloadProgressBar.gameObject.SetActive(true);
            yield return new WaitForSeconds(reloadTime);
            currentCapacity = capacity;
            uiCurrentCapacity.text = currentCapacity.ToString();
        }
    }

    private void UpdateRotation()
    {
        var mousePos = Input.mousePosition;
        var percX = Mathf.Clamp(mousePos.x / Screen.width, 0.0f, 0.5f);
        var percY = 1 - Mathf.Clamp(mousePos.y / Screen.height, 0.0f, 0.5f);

        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, (-percX * 20) + (percY * 12));
    }
}
