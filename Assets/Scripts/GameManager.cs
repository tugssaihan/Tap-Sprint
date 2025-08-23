using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject countDownPanel;
    [SerializeField] TMP_Text countDownText;
    PlayerMovement playerMovement;
    InputActions inputActions;

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
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        countDownPanel.SetActive(true);
        StartCoroutine(StartCountDown());
    }
}
