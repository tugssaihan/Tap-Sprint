using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float currentSpeed = 0f;
    [SerializeField] float speedPerClick = 0.5f;

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
        // increase speed
        if (inputActions.Player.Move.WasPressedThisFrame())
        {
            Debug.Log("Clicked!");
            currentSpeed += speedPerClick;
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
