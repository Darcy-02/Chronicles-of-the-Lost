
using UnityEngine;

public class FightingCameraController : MonoBehaviour
{
    [Header("Fighters")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemy;

    [Header("Camera Follow")]
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float lookHeight = 1.2f;

    [Header("Camera Shake")]
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeMagnitude = 0.12f;

    private Vector3 cameraDirection;
    private float cameraDistance;
    private float shakeTimer;
    private Vector3 shakeOffset;

    private void Start()
    {
        if (player == null || enemy == null)
        {
            Debug.LogError("Assign both fighters!");
            enabled = false;
            return;
        }

        Vector3 midpoint = GetMidpoint();
        Vector3 offset = transform.position - midpoint;

        cameraDistance = offset.magnitude;
        cameraDirection = offset.normalized;
    }

    private void LateUpdate()
    {
        if (player == null || enemy == null) return;

        Vector3 midpoint = GetMidpoint();

        Vector3 targetPosition =
            midpoint + cameraDirection * cameraDistance;

        UpdateShake();

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition + shakeOffset,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(midpoint + Vector3.up * lookHeight);
    }

    private Vector3 GetMidpoint()
    {
        return (player.position + enemy.position) / 2f;
    }

    public void ShakeCamera()
    {
        shakeTimer = shakeDuration;
    }

    private void UpdateShake()
    {
        if (shakeTimer > 0f)
        {
            shakeOffset =
                Random.insideUnitSphere * shakeMagnitude;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
        }
    }
}
