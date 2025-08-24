using System;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] float aiCurrentSpeed = 0f;
    float aiMoveSpeed;
    float timer = 0f;
    float waitTime = 0.15f;
    bool deaccelerateLine = false;
    bool finished = false;
    float decreaseTime = 0.5f;
    float time;

    void Start()
    {
        aiMoveSpeed = UnityEngine.Random.Range(0.5f, 2f);
        Debug.Log($"{gameObject.name}: {aiMoveSpeed}");
    }

    void Update()
    {
        MoveAI();
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
