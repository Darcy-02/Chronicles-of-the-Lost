
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

        Debug.Log(
            gameObject.name + " TARGET: " +
            (opponent != null ? opponent.name : "NULL")
        );
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
        Debug.Log("PUNCH EVENT: " + gameObject.name);
        Debug.Log("PENDING ATTACK: " + pendingAttack);

        if (pendingAttack != AttackType.Punch)
        {
            Debug.LogWarning("PUNCH CANCELLED: Wrong pending attack!");
            return;
        }

        pendingAttack = AttackType.None;
        ApplyHit(punchDamage);
    }

    public void KickHitFrame()
    {
        if (pendingAttack != AttackType.Kick) return;

        pendingAttack = AttackType.None;
        ApplyHit(kickDamage);
    }

    public void SetFighter(FighterBase self)
    {
        fighter = self;
    }

    private void ApplyHit(float damage)
    {
        Debug.Log(gameObject.name + " APPLYING DAMAGE");
        if (opponent == null || opponent.IsDefeated)
        {
            Debug.Log("NO VALID OPPONENT!");
            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            opponent.transform.position
        );
        Debug.Log("DISTANCE TO PLAYER: " + distance);

        if (distance > combatRange)
        {
            Debug.Log("TARGET TOO FAR!");
            return;
        }
            

        opponent.TakeDamage(damage);
    }
}

