
using UnityEngine;
using System;

public abstract class FighterBase : MonoBehaviour, IFighter
{
    [Header("References")]
    [SerializeField] private FighterBase opponent;
    [SerializeField] private FighterHealth health;
    [SerializeField] private FighterCombat combat;
    [SerializeField] private FighterDefense defense;
    [SerializeField] private FighterEffects fighterEffects;
    [SerializeField] private FighterAudio fighterAudio;

    private bool canFight = true;

    protected FighterAnimation fighterAnimation;

    public bool IsDefeated => health != null && health.IsDefeated;

    public bool OpponentIsDefeated =>
        opponent != null && opponent.IsDefeated;

    protected bool CanFight => canFight && !IsDefeated;

    public event Action OnFighterDefeated;

    protected virtual void Start()
    {
       
        if (health == null)
            health = GetComponent<FighterHealth>();

        if (combat == null)
            combat = GetComponent<FighterCombat>();

        if (defense == null)
            defense = GetComponent<FighterDefense>();

        if (fighterAnimation == null)
            fighterAnimation = GetComponent<FighterAnimation>();

        if (fighterEffects == null)
            fighterEffects = GetComponent<FighterEffects>();

        if (fighterAudio == null)
            fighterAudio = GetComponent<FighterAudio>();

        if (health != null)
        {
            health.OnDamaged += HandleDamaged;
            health.OnDefeated += HandleDefeated;
        }
    }

    private bool temporarilyDisableFacing;

    public void SetTemporaryFacing(bool value)
    {
        temporarilyDisableFacing = value;
    }

    protected virtual void LateUpdate()
    {
        if (temporarilyDisableFacing) return;

        if (opponent == null) return;

        Vector3 direction = opponent.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnDamaged -= HandleDamaged;
            health.OnDefeated -= HandleDefeated;
        }
    }

    public void SetCanFight(bool value)
    {
        canFight = value;
    }

    public virtual void Attack()
    {
        if (!CanFight || combat == null) return;

        StopBlock();
        combat.Punch();
    }

    public virtual void Kick()
    {
        if (!CanFight || combat == null) return;

        StopBlock();
        combat.Kick();
    }

    public void PunchHitFrame()
    {
        if (combat != null)
            combat.PunchHitFrame();
    }

    public void KickHitFrame()
    {
        if (combat != null)
            combat.KickHitFrame();
    }

    public virtual void Block()
    {
        if (!CanFight || defense == null) return;

        defense.Block();
    }

    public virtual void StopBlock()
    {
        if (defense != null)
            defense.StopBlock();
    }

    public virtual void TakeDamage(float damage)
    {
        if (health == null || IsDefeated) return;

        health.TakeDamage(damage, defense != null && defense.IsBlocking);
    }

    private void HandleDamaged(float damage, bool wasBlocking)
    {
        if (wasBlocking)
        {
            fighterAudio?.PlayBlock();
        }
        else
        {
            fighterAudio?.PlayHit();
            fighterAnimation?.PlayHit();
        }

        fighterEffects?.PlayHitEffect();
        OnFighterDefeated?.Invoke();
    }

    private void HandleDefeated()
    {
        fighterAnimation?.PlayDeath();
        fighterAudio?.PlayDeath();
        fighterEffects?.PlayDeathEffect();

        if (opponent != null)
        {
            opponent.fighterAnimation?.PlayVictory();
           
        }

        StopBlock();

        Debug.Log(gameObject.name + " has been defeated!");
    }

    public float GetHealth()
    {
        return health != null ? health.GetHealth() : 0f;
    }
}
