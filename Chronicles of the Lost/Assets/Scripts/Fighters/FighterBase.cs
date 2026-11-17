
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
    [SerializeField] private HealthBar healthBar;

    protected Animator animator;

    private float currentHealth;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isBlocking;
    private bool isDefeated;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public virtual void Attack()
    {
        if (isDefeated || !CanAttack()) return;

        StopBlock();
        animator.SetTrigger("Punch");

        if (IsOpponentInRange())
        {
            opponent.TakeDamage(attackDamage);
        }
    }

    public virtual void Kick()
    {
        if (isDefeated || !CanAttack()) return;

        StopBlock();
        animator.SetTrigger("Kick");

        if (IsOpponentInRange())
        {
            opponent.TakeDamage(kickDamage);
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
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log(gameObject.name + " Health: " + currentHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            isDefeated = true;
            Debug.Log(gameObject.name + " has been defeated!");
            StopBlock();
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
}
