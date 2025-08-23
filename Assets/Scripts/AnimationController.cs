using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    InputActions inputActions;
    bool hasFinished = false;
    bool isRunning = false;

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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (inputActions.Player.Move.IsPressed() && !hasFinished && !isRunning)
        {
            Debug.Log("resetting animation");
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish Line"))
        {
            hasFinished = true;
            animator.SetBool("isRunning", false);
        }
    }
}
