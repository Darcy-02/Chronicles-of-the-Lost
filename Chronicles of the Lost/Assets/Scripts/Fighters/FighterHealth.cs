
using System;
using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;

    [Header("References")]
    [SerializeField] private HealthBar healthBar;

    private float currentHealth;
    private bool isDefeated;

    public event Action<float, bool> OnDamaged;
    public event Action OnDefeated;

    public float CurrentHealth => currentHealth;
    public bool IsDefeated => isDefeated;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(float damage, bool isBlocking)
    {
        if (isDefeated || damage <= 0f)
            return;

        if (isBlocking)
            damage *= 0.25f;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0f, currentHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log(gameObject.name + " Health: " + currentHealth);

        OnDamaged?.Invoke(damage, isBlocking);

        if (currentHealth <= 0f)
        {
            isDefeated = true;
            OnDefeated?.Invoke();
        }
    }

    public void SetHealthBar(HealthBar bar)
    {
        healthBar = bar;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}

