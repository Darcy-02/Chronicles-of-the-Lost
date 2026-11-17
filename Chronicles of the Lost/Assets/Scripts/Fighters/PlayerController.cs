
using UnityEngine;

public class PlayerController : FighterBase
{
    private FighterMovement movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<FighterMovement>();
    }

    void Update()
    {

        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(0f, 0f, vertical);

        movement.Move(direction);

        if (direction == Vector3.zero)
        {
            GetComponent<Animator>().SetFloat("Speed", 0f);
        }

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
