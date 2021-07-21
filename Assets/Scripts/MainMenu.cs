using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public UnityEvent onReadyToStart;

    public void OnClickStart()
    {
        StartCoroutine(HandleClickStart());
    }

    private IEnumerator HandleClickStart()
    {
        var animator = GetComponent<Animator>();
        animator.Play("MainMenuEnd");
        yield return new WaitForSeconds(0.5f);
        onReadyToStart.Invoke();
        gameObject.SetActive(false);
    }
}
