using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float currentSpeed = 0f;
    [SerializeField] float speedPerClick = 1f;

    InputActions inputActions;

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Awake()
    {
        inputActions = new InputActions();   
    }

    void Update()
    {
        // Debug.Log("Player current speed = " + currentSpeed);
        // increase speed
        if (inputActions.Player.Move.WasPressedThisFrame())
        {
            currentSpeed += speedPerClick;
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
