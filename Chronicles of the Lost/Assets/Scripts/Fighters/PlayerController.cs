
using UnityEngine;

public class PlayerController : FighterBase
{
    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 720f;

    private FighterMovement movement;

    private float mobileHorizontal;
    private float mobileVertical;
    private bool mobileBlocking;
    private ComboExecutor comboExecutor;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<FighterMovement>();
        comboExecutor = GetComponentInChildren<ComboExecutor>(true);
    }

    private void Update()
    {
        if (!CanFight) return;

        float horizontal = mobileHorizontal;
        float vertical = mobileVertical;


        if (Input.GetKey(KeyCode.A)) horizontal = -1f;
        if (Input.GetKey(KeyCode.D)) horizontal = 1f;
        if (Input.GetKey(KeyCode.S)) vertical = -1f;
        if (Input.GetKey(KeyCode.W)) vertical = 1f;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        bool movingBackward = vertical < 0f;
        SetTemporaryFacing(movingBackward);

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

        if (Input.GetKey(KeyCode.L) || mobileBlocking)
            Block();
        else
            StopBlock();
    }



    public void SetMobileMovement(float horizontal, float vertical)
    {
        mobileHorizontal = horizontal;
        mobileVertical = vertical;
    }

    public void MobilePunch()
    {
        if (CanFight) Attack();
    }

    public void MobileKick()
    {
        if (CanFight) Kick();
    }

    public void MobileBlock()
    {
        mobileBlocking = true;
    }

    public void MobileStopBlock()
    {
        mobileBlocking = false;
    }

    public void MobileCombo()
    {
        if (CanFight && comboExecutor != null)
            comboExecutor.GenerateAndPlayCombo();
    }
}
