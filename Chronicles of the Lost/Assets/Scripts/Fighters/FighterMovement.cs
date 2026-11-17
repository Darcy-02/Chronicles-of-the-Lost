
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;


    public void Move(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        transform.position += direction * moveSpeed * Time.deltaTime;

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetFloat("Speed", direction.magnitude);
        }
    }

}
