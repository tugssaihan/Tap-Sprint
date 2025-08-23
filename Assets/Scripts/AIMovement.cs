using System;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] float aiCurrentSpeed = 0f;
    float aiMoveSpeed;
    float timer = 0f;
    float waitTime = 0.2f;
    bool deaccelerateLine = false;
    bool finished = false;
    float decreaseTime = 1f;

    void Start()
    {
        aiMoveSpeed = UnityEngine.Random.Range(0.5f, 1.2f);
    }

    void Update()
    {
        MoveAI();
    }

    void MoveAI()
    {
        Debug.Log("ai current speed: " + aiCurrentSpeed);
        timer += Time.deltaTime;
        if (timer >= waitTime && !finished)
        {
            aiCurrentSpeed += aiMoveSpeed;
            timer = 0f;
        }

        // decrease speed
        if (aiCurrentSpeed >= 20 || finished)
        {
            aiCurrentSpeed -= decreaseTime * Time.deltaTime;
        }

        // limit speed 30 until slow down line
            if (!deaccelerateLine)
            {
                aiCurrentSpeed = Mathf.Clamp(aiCurrentSpeed, 0, 30);
            }
            else
            {
                aiCurrentSpeed = Mathf.Clamp(aiCurrentSpeed, 0, 20);
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
            finished = true;
            decreaseTime = 15f;
            aiCurrentSpeed = 15f;
        }
    }
}
