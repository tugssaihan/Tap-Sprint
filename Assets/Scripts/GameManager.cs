using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject countDownPanel;
    [SerializeField] GameObject finishLine;
    [SerializeField] TMP_Text countDownText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text distanceText;
    [SerializeField] TMP_Text timeText;

    PlayerMovement playerMovement;
    InputActions inputActions;
    bool gameStarted = false;
    float startTime;
    float raceTime;

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

        playerMovement.SetInputActions(inputActions);
        countDownPanel.SetActive(false);
        Time.timeScale = 1f;
        inputActions.Enable();
        gameStarted = true;
    }

    void Update()
    {
        if (gameStarted)
        {
            ShowTexts();
        }
    }

    public void ShowTexts()
    {
        speedText.text = "Speed: " + playerMovement.currentSpeed.ToString("F2");

        float distanceLeft = (finishLine.transform.position.z - playerMovement.gameObject.transform.position.z - 41) / 2;
        distanceText.text = "Distance: " + Mathf.RoundToInt(distanceLeft);

        raceTime = Time.time - startTime;
        timeText.text = "Time: " + raceTime.ToString("F2");
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        countDownPanel.SetActive(true);
        StartCoroutine(StartCountDown());
    }
}
