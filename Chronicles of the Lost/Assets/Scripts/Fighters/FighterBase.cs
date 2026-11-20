
using UnityEngine;

public abstract class FighterBase : MonoBehaviour, IFighter
{
    [Header("Fighter Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float kickDamage = 20f;

    [Header("Combat Settings")]
    [SerializeField] private float combatRange = 2.5f;
    [SerializeField] private float attackCooldown = 0.8f;

    [SerializeField] private FighterBase opponent;
    public bool OpponentIsDefeated => opponent != null && opponent.IsDefeated;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private DissolvingController dissolvingController;

    [SerializeField] private FightingCameraController cameraController;

    protected Animator animator;

    protected virtual void LateUpdate()
    {
        if (opponent == null) return;

        Vector3 direction = opponent.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private float currentHealth;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isBlocking;
    private bool isDefeated;

    public bool IsDefeated => isDefeated;

    private enum AttackType { None, Punch, Kick }
    private AttackType pendingAttack = AttackType.None;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    public virtual void Attack()
    {
        if (isDefeated || !CanAttack()) return;

        StopBlock();
        pendingAttack = AttackType.Punch;
        animator.SetTrigger("Punch");
    }

    public virtual void Kick()
    {
        if (isDefeated || !CanAttack()) return;

        StopBlock();
        pendingAttack = AttackType.Kick;
        animator.SetTrigger("Kick");
    }


    public void PunchHitFrame()
    {
        if (pendingAttack != AttackType.Punch) return;

        pendingAttack = AttackType.None;

        if (IsOpponentInRange() &&
            IsFacingOpponent() &&
            opponent.IsFacingOpponent())
        {
            AudioManager.Instance?.PlayPunch();
            opponent.TakeDamage(attackDamage);
        }
    }


    public void KickHitFrame()
    {
        if (pendingAttack != AttackType.Kick) return;

        pendingAttack = AttackType.None;

        if (IsOpponentInRange() &&
            IsFacingOpponent() &&
            opponent.IsFacingOpponent())
        {
            AudioManager.Instance?.PlayKick();
            opponent.TakeDamage(kickDamage);
            opponent.animator.SetTrigger("KickHit");
        }
    }

    private bool CanAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return false;

        lastAttackTime = Time.time;
        return true;
    }

    private bool IsOpponentInRange()
    {
        if (opponent == null) return false;

        float distance = Vector3.Distance(
            transform.position,
            opponent.transform.position
        );

        return distance <= combatRange;
    }

    public virtual void Block()
    {
        if (isDefeated) return;

        isBlocking = true;
        animator.SetBool("Block", true);
    }

    public virtual void StopBlock()
    {
        isBlocking = false;
        animator.SetBool("Block", false);
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDefeated) return;

        if (isBlocking)
        {
            damage *= 0.25f;
            AudioManager.Instance?.PlayBlock();
        }
        else
        {
            AudioManager.Instance?.PlayHit();
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log(gameObject.name + " Health: " + currentHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (cameraController != null)
            cameraController.ShakeCamera();

        if (currentHealth <= 0)
        {
            isDefeated = true;

            animator.SetTrigger("Death");

            AudioManager.Instance?.PlayDeath();

            if (dissolvingController != null)
                dissolvingController.StartDissolve();

            if (opponent != null)
            {
                opponent.animator.SetTrigger("Victory");
                
            }

            Debug.Log(gameObject.name + " has been defeated!");

            StopBlock();
        }
        else if (!isBlocking)
        {
            animator.SetTrigger("Hit");
        }
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public float GetKickDamage()
    {
        return kickDamage;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    private bool IsFacingOpponent()
    {
        if (opponent == null) return false;

        Vector3 direction = opponent.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f) return false;

        return Vector3.Dot(transform.forward, direction.normalized) > 0.5f;
    }
}
