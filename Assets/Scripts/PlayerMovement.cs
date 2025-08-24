using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float currentSpeed = 0f;
    [SerializeField] float speedPerClick = 0.2f;

    InputActions inputActions;
    GameManager gameManager;
    float decreaseSpeed = 0.1f;
    bool deaccelerateLine = false;
    public bool playerFinished = false;

    public void SetInputActions(InputActions actions)
    {
        inputActions = actions;
    }

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (inputActions == null) return;
        
        // increase speed
        if (inputActions.Player.Move.IsPressed())
        {
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
            currentSpeed = Mathf.Clamp(currentSpeed, 0, 25);
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slow"))
        {
            deaccelerateLine = true;
            speedPerClick /= 2;
        }
        else if (other.CompareTag("Finish Line"))
        {
            gameManager.AddResults(gameObject.name, gameManager.raceTime);
            playerFinished = true;
            inputActions.Disable();

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayCheering();
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayRunning(false);

            gameManager.finishedRacers += 1;
            currentSpeed = 18f;
        }
    }
}
