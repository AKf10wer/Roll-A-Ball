using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime = 0;
    float bestTime;
    bool timing = false;

    SceneController sceneController;

    [Header("UI Countdown Panel")]
    public GameObject countdownPanel;
    public Text countdownText;

    [Header("UI Game Over Panel")]
    public GameObject timesPanel;
    public Text myTimeResult;
    public Text bestTimeResult;
    void Start()
    {
        timesPanel.SetActive(false);
        countdownPanel.SetActive(false);
        timerText.text = "";
        sceneController = FindObjectOfType<SceneController>();
    }

    void Update()
    {
        if (timing)
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("F3");
        }
    }

    public IEnumerator StartCountdown()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime" + sceneController.GetSceneName());
        if (bestTime == 0f) bestTime = 600f;

        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);
    }

    public void StartTimer()
    {
        currentTime = 0;
        timing = true; 
    }

    public void StopTimer()
    {
        timing = false;
        timesPanel.SetActive(true);
        myTimeResult.text = currentTime.ToString("F3");
        bestTimeResult.text = bestTime.ToString("F3");

        if (currentTime <= bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime" + sceneController.GetSceneName(), bestTime);
            bestTimeResult.text = bestTime.ToString("F3") + " !! NEW BEST !!";
        }

        public bool IsTiming()
            {
            return timing;
        })
    }
}
