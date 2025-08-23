using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator animator;
    bool isRunning = false;
    bool hasFinished = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isRunning && !hasFinished)
        {
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
