
using UnityEngine;

public class FighterAI : FighterBase
{
    [Header("AI Settings")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float decisionInterval = 1.5f;
    [SerializeField] private float moveSpeed = 2f;

    private AIDecisionMaker decisionMaker;
    private float decisionTimer;
    private FighterMovement movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<FighterMovement>();
        decisionMaker = new AIDecisionMaker();
    }


    void Update()
    {
        if (playerTarget == null) return;

        float distance = Vector3.Distance(
            transform.position,
            playerTarget.position
        );

        if (distance > attackRange)
        {
            StopBlocking();
            MoveTowardsPlayer();
        }
        else
        {
            decisionTimer += Time.deltaTime;

            if (decisionTimer >= decisionInterval)
            {
                decisionTimer = 0f;
                MakeDecision();
            }
        }
    }

    void StopBlocking()
    {
        StopBlock();
    }


    void MakeDecision()
    {
        float distance = Vector3.Distance(
            transform.position,
            playerTarget.position
        );

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

    void MoveTowardsPlayer()
    {
        Vector3 direction = playerTarget.position - transform.position;

        direction.x = 0f;
        direction.y = 0f;

        movement.Move(direction);
    }
}
