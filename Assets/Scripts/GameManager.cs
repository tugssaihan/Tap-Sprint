using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject countDownPanel;
    [SerializeField] GameObject scoreboardPanel;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text distanceText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject finishLine;
    [SerializeField] TMP_Text[] placementTexts;

    PlayerMovement playerMovement;
    InputActions inputActions;

    bool gameStarted = false;
    float startTime;
    public float raceTime;
    public float raceStartTime;
    public int finishedRacers = 0;
    public Dictionary<string, string> results = new Dictionary<string, string>();

    void Awake()
    {
        inputActions = new InputActions();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Start()
    {
        Time.timeScale = 0f;
        inputActions.Disable();
        startPanel.SetActive(true);
        countDownPanel.SetActive(false);
        scoreboardPanel.SetActive(false);

        startTime = Time.time;
    }

    IEnumerator StartCountDown()
    {
        int count = 3;
        while (count > 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }

        countDownText.text = "GO! GO! GO!";
        yield return new WaitForSecondsRealtime(1f);
        countDownText.text = "";

        raceStartTime = Time.time;

        playerMovement.SetInputActions(inputActions);
        countDownPanel.SetActive(false);
        Time.timeScale = 1f;
        inputActions.Enable();
        gameStarted = true;
        ShowTexts();
    }

    void Update()
    {
        if (gameStarted && !playerMovement.playerFinished)
        {
            ChangeTexts();
        }
        if (finishedRacers >= 7)
        {
            GameOver();
        }
    }

    private void ChangeTexts()
    {
        speedText.text = "Speed: " + playerMovement.currentSpeed.ToString("F2");

        float sceneLength = 241f;
        float raceMeters = 100f;

        float playerZ = playerMovement.transform.position.z;
        float distanceLeft = (sceneLength - playerZ) * (raceMeters / sceneLength);

        distanceText.text = "Distance: " + Mathf.RoundToInt(distanceLeft);

        raceTime = Time.time - startTime;
        timeText.text = "Time: " + raceTime.ToString("F2");
    }

    void ShowTexts()
    {
        speedText.gameObject.SetActive(true);
        distanceText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        countDownPanel.SetActive(true);
        StartCoroutine(StartCountDown());
    }

    public void GameOver()
    {
        HideTexts();
        ShowResults();
        scoreboardPanel.SetActive(true);
    }

    private void ShowResults()
    {
        int index = 1;
        foreach (KeyValuePair<string, string> result in results)
        {
            placementTexts[index - 1].text = $"{index}. {result.Key}: {result.Value}";
            Debug.Log($"{index++}. {result.Key}: {result.Value}");
        }
    }

    public void AddResults(string name, float time)
    {
        string roundedTime = time.ToString("F2");
        results.Add(name, roundedTime);
    }

    private void HideTexts()
    {
        speedText.gameObject.SetActive(false);
        distanceText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }
}
