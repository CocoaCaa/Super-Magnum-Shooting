using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Animator uiBeginAnimator;
    public GameObject uiInGame;
    public TextMeshProUGUI uiScore;
    public TextMeshProUGUI uiCountdown;
    public TextMeshProUGUI uiRapidFire;
    public AudioClip rapidFireStartSound;
    public GameObject timesUpModal;
    public Gun gun;
    public Canvas uiCanvas;
    public GameObject prefabTargetScoreText;
    public MoleCrateSpawner moleCrateSpawner;

    private int totalScore = 0;
    private float leftTime = 60.0f;
    private bool isStarted = false;
    private bool isRapidFire = false;
    private float rapidFireLeftTime = 10.0f;
    private float originalGunFireRate = 0.0f;

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
        moleCrateSpawner.StartSpawn();
        Cursor.visible = false;
    }

    public void HandleHitScore(Target target, Vector3 hitPoint)
    {
        totalScore += target.score;
        uiScore.text = totalScore.ToString();
        var targetScoreText = Instantiate(prefabTargetScoreText, uiCanvas.transform);
        targetScoreText.GetComponent<TargetScoreText>().SetToTarget(uiCanvas.GetComponent<RectTransform>(), target.score, hitPoint);
    }

    public void HandleMoleCrateHit()
    {
        if (isRapidFire)
        {
            return;
        }
        isRapidFire = true;
        originalGunFireRate = gun.fireRate;
        gun.fireRate = 0.1f;
        uiRapidFire.gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(rapidFireStartSound);
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

        if (isRapidFire)
        {
            rapidFireLeftTime = Mathf.Max(rapidFireLeftTime - Time.deltaTime, 0);
            updateRapidFireText();
            if (rapidFireLeftTime <= 0)
            {
                isRapidFire = false;
                uiRapidFire.gameObject.SetActive(false);
                gun.fireRate = originalGunFireRate;
            }
        }
    }

    private void updateRapidFireText()
    {
        var op = LocalizationSettings.StringDatabase.GetLocalizedStringAsync(
            "Main Text",
            "inGame.rapidFire",
            new string[] { rapidFireLeftTime.ToString("0.0s") }
        );
        if (op.IsDone)
        {
            uiRapidFire.text = op.Result;
        }
        else
        {
            op.Completed += (o) => uiRapidFire.text = o.Result;
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
