using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Animator uiBeginAnimator;
    public GameObject uiInGame;
    public TextMeshProUGUI uiScore;

    private int totalScore = 0;

    public void StartGame()
    {
        Reset();
        StartCoroutine(HandleStartGame());
    }

    private IEnumerator HandleStartGame()
    {
        uiBeginAnimator.gameObject.SetActive(true);
        uiBeginAnimator.Play("BeginUI");
        yield return new WaitForSeconds(4f);
        uiInGame.SetActive(true);
    }

    public void HandleHitScore(int score)
    {
        totalScore += score;
        uiScore.text = totalScore.ToString();
    }

    private void Reset()
    {
        totalScore = 0;
    }
}
