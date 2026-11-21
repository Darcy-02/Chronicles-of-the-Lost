using UnityEngine;

public class FighterAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayPunch()
    {
        animator.SetTrigger("Punch");
    }

    public void PlayKick()
    {
        animator.SetTrigger("Kick");
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