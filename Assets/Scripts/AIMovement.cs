using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [SerializeField] float aiCurrentSpeed = 0f;

    void Update()
    {
        MoveAI();
    }

    void MoveAI()
    {
        // Debug.Log("ai current speed: " + aiCurrentSpeed);
        aiCurrentSpeed += Random.Range(0.5f, 1f);
        // decrease speed over time
        aiCurrentSpeed -= 0.5f;

        transform.Translate(Vector3.forward * aiCurrentSpeed * Time.deltaTime);
    }
}
