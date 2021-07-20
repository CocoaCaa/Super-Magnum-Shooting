using System.Collections;
using UnityEngine;

public class ReloadProgressBar : MonoBehaviour
{
    public float time = 1.3f;

    void OnEnable()
    {
        StartCoroutine(HandleTimeout());
    }

    private IEnumerator HandleTimeout()
    {
        GetComponent<Animator>().speed = 1 / time;
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
