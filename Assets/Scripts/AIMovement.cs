using System;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] public float aiCurrentSpeed = 0f;

    GameManager gameManager;
    float aiMoveSpeed;
    float timer = 0f;
    float waitTime = 0.15f;
    bool deaccelerateLine = false;
    bool finished = false;
    float decreaseTime = 0.5f;
    float enemyFinishTime;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        aiMoveSpeed = UnityEngine.Random.Range(1.5f, 4.5f);
    }

    void Update()
    {
        if (!finished)
        {
            CalculateTime();
        }

        MoveAI();
    }

    private void CalculateTime()
    {
        enemyFinishTime = Time.time - gameManager.raceStartTime;
    }

    void MoveAI()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime && !finished)
        {
            aiCurrentSpeed += aiMoveSpeed;
            timer = 0f;
        }

        // decrease speed
        if (finished)
        {
            aiCurrentSpeed -= decreaseTime * Time.deltaTime;
        }

        // limit speed 35 until slow down line
        if (!deaccelerateLine)
        {
            aiCurrentSpeed = Mathf.Clamp(aiCurrentSpeed, 0, 35);
        }
        else
        {
            float randomSpeedLimit = UnityEngine.Random.Range(20, 25);
            aiCurrentSpeed = Mathf.Clamp(aiCurrentSpeed, 0, randomSpeedLimit);
        }

        transform.Translate(Vector3.forward * aiCurrentSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slow"))
        {
            deaccelerateLine = true;
            aiMoveSpeed /= 2;
            
        }
        else if (other.CompareTag("Finish Line"))
        {
            gameManager.AddResults(gameObject.name, enemyFinishTime);
            gameManager.finishedRacers += 1;
            finished = true;
            decreaseTime = 15f;
            aiCurrentSpeed = 15f;
        }
    }
}
