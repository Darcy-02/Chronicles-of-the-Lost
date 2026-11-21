
using UnityEngine;

public class PlayerController : FighterBase
{
    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 720f;

    private FighterMovement movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<FighterMovement>();
    }

    private void Update()
    {
        if (!CanFight) return;

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;
        if (Input.GetKey(KeyCode.W)) vertical = 1f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // Temporarily stop automatic opponent-facing while moving backward.
        bool movingBackward = vertical < 0f;
        SetTemporaryFacing(movingBackward);

        // Turn towards the direction of backward movement.
        if (movingBackward && direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction.normalized);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        movement.Move(direction);

        fighterAnimation.SetSpeed(direction.magnitude);

        if (Input.GetKeyDown(KeyCode.J))
            Attack();

        if (Input.GetKeyDown(KeyCode.K))
            Kick();

        if (Input.GetKey(KeyCode.L))
            Block();
        else
            StopBlock();
    }
}
