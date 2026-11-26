
using UnityEngine;

public class FighterAI : FighterBase
{
    [Header("AI Settings")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float decisionInterval = 1.5f;
    [SerializeField] private float rotationSpeed = 720f;

    private AIDecisionMaker decisionMaker;
    private float decisionTimer;
    private FighterMovement movement;

    protected override void Start()
    {
        base.Start();

        movement = GetComponent<FighterMovement>();
        decisionMaker = new AIDecisionMaker();
    }

    private void Awake()
    {
        Debug.Log("FIGHTER AI FOUND ON: " + gameObject.name);
    }

    private void Update()
    {
        if (!CanFight) return;

        if (playerTarget == null || OpponentIsDefeated)
            return;

        // Always face Player 1
        FacePlayer();

        float distance = Vector3.Distance(
            transform.position,
            playerTarget.position
        );

        if (distance > attackRange)
        {
            StopBlock();
            MoveTowardsPlayer();
            return;
        }

        decisionTimer += Time.deltaTime;

        if (decisionTimer >= decisionInterval)
        {
            decisionTimer = 0f;
            MakeDecision(distance);
        }
    }


    private void FacePlayer()
    {
        Vector3 direction = playerTarget.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void MakeDecision(float distance)
    {
        AIDecisionMaker.Action action =
            decisionMaker.Decide(distance, GetHealth());

        switch (action)
        {
            case AIDecisionMaker.Action.MoveCloser:
                MoveTowardsPlayer();
                break;

            case AIDecisionMaker.Action.Punch:
                Attack();
                break;

            case AIDecisionMaker.Action.Kick:
                Kick();
                break;

            case AIDecisionMaker.Action.Block:
                Block();
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = playerTarget.position - transform.position;
        direction.y = 0f;

        movement.Move(direction);
    }
}
