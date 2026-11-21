
using UnityEngine;

public class FighterCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private float combatRange = 2.5f;
    [SerializeField] private float attackCooldown = 0.8f;
    [SerializeField] private float punchDamage = 10f;
    [SerializeField] private float kickDamage = 20f;
    [SerializeField] private FighterAudio fighterAudio;

    [Header("References")]
    [SerializeField] private FighterBase fighter;
    [SerializeField] private FighterBase opponent;

    [SerializeField] private FighterAnimation fighterAnimation;
    private float lastAttackTime = -Mathf.Infinity;

    private enum AttackType { None, Punch, Kick }
    private AttackType pendingAttack = AttackType.None;

    private void Awake()
    {
        if (fighterAnimation == null)
            fighterAnimation = GetComponent<FighterAnimation>();

        if (fighter == null)
            fighter = GetComponent<FighterBase>();

        if (fighterAudio == null)
            fighterAudio = GetComponent<FighterAudio>();
    }

    public void SetOpponent(FighterBase target)
    {
        opponent = target;
    }

    public void Punch()
    {
        if (!CanAttack()) return;

        pendingAttack = AttackType.Punch;
        fighterAnimation?.PlayPunch();
        fighterAudio?.PlayPunch();
    }

    public void Kick()
    {
        if (!CanAttack()) return;

        pendingAttack = AttackType.Kick;
        fighterAnimation?.PlayKick();
        fighterAudio?.PlayKick();
    }

    private bool CanAttack()
    {
        if (fighter == null || fighter.IsDefeated)
            return false;

        if (Time.time - lastAttackTime < attackCooldown)
            return false;

        lastAttackTime = Time.time;
        return true;
    }

    public void PunchHitFrame()
    {
        if (pendingAttack != AttackType.Punch) return;

        pendingAttack = AttackType.None;
        ApplyHit(punchDamage);
    }

    public void KickHitFrame()
    {
        if (pendingAttack != AttackType.Kick) return;

        pendingAttack = AttackType.None;
        ApplyHit(kickDamage);
    }

    private void ApplyHit(float damage)
    {
        if (opponent == null || opponent.IsDefeated)
            return;

        float distance = Vector3.Distance(
            transform.position,
            opponent.transform.position
        );

        if (distance > combatRange)
            return;

        opponent.TakeDamage(damage);
    }
}

