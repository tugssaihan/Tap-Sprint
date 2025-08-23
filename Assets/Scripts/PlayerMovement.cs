using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float currentSpeed = 0f;
    [SerializeField] float speedPerClick = 0.2f;

    InputActions inputActions;
    float decreaseSpeed = 0.1f;
    bool deaccelerateLine = false;

    public void SetInputActions(InputActions actions)
    {
        inputActions = actions;
    }

    void Update()
    {
        if (inputActions == null) return;
        
        // increase speed
        if (inputActions.Player.Move.IsPressed())
        {
            Debug.Log("pressing");
            currentSpeed += speedPerClick;
        }
        // decrease speed
        else
        {
            currentSpeed -= decreaseSpeed;
        }

        // max speed 30 until deaccelerateLine
        if (!deaccelerateLine)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 30);
        }
        // slow down after triggering deaccelerateLine
        else
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 20);
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slow"))
        {
            Debug.Log("slowing down");
            deaccelerateLine = true;
            speedPerClick /= 2;
        }
        else if (other.CompareTag("Finish Line"))
        {
            currentSpeed = 18f;
        }
    }
}
