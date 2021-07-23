using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Animator uiBeginAnimator;
    public GameObject uiInGame;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI uiCountdown;
    public GameObject timesUpModal;
    public Gun gun;
    public Canvas uiCanvas;
    public GameObject prefabTargetScoreText;

    private int totalScore = 0;
    private float leftTime = 60.0f;
    private bool isStarted = false;

    public void StartGame()
    {
        StartCoroutine(HandleStartGame());
    }

    private IEnumerator HandleStartGame()
    {
        uiBeginAnimator.gameObject.SetActive(true);
        uiBeginAnimator.Play("BeginUI");
        yield return new WaitForSeconds(4f);
        isStarted = true;
        uiInGame.SetActive(true);
        Cursor.visible = false;
    }

    public void HandleHitScore(Target target, Vector3 hitPoint)
    {
        totalScore += target.score;
        uiScore.text = totalScore.ToString();
        var targetScoreText = Instantiate(prefabTargetScoreText, uiCanvas.transform);
        targetScoreText.GetComponent<TargetScoreText>().SetToTarget(uiCanvas.GetComponent<RectTransform>(), target.score, hitPoint);
    }

    private void Update()
    {
        if (isStarted)
        {
            leftTime = Mathf.Max(leftTime - Time.deltaTime, 0);
            uiCountdown.text = leftTime.ToString("0.0s");
            if (leftTime <= 0)
            {
                HandleEndedGame();
                isStarted = false;
            }
        }
    }

    private void HandleEndedGame()
    {
        gun.isLocked = true;
        timesUpModal.SetActive(true);
        Cursor.visible = true;
    }

    public void HandleOnRestart()
    {
        StartCoroutine(HandleRestart());
    }

    private IEnumerator HandleRestart()
    {
        uiInGame.GetComponent<Animator>().Play("InGameExit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainScene");
    }
}
