
using UnityEngine;

public class FighterAnimation : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Animation Controller Type")]
    [SerializeField] private bool useMultipleAttacks;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void PlayPunch()
    {
        if (useMultipleAttacks)
        {
            int punchNumber = Random.Range(1, 4);
            animator.SetTrigger("Punch" + punchNumber);
        }
        else
        {
            animator.SetTrigger("Punch");
        }
    }

    public void PlayKick()
    {
        if (useMultipleAttacks)
        {
            int kickNumber = Random.Range(1, 4);
            animator.SetTrigger("Kick" + kickNumber);
        }
        else
        {
            animator.SetTrigger("Kick");
        }
    }

    public void PlayBlock(bool isBlocking)
    {
        animator.SetBool("Block", isBlocking);
    }

    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    public void PlayDeath()
    {
        animator.SetTrigger("Death");
    }

    public void PlayVictory()
    {
        animator.SetTrigger("Victory");
    }

    public void SetSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
}
