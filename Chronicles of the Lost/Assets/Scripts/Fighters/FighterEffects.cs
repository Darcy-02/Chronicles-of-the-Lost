using UnityEngine;

public class FighterEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitParticlePrefab;
    [SerializeField] private DissolvingController dissolvingController;
    [SerializeField] private FightingCameraController cameraController;

    public void PlayHitEffect()
    {
        if (cameraController != null)
            cameraController.ShakeCamera();

        if (hitParticlePrefab == null) return;

        Vector3 position = transform.position + Vector3.up * 1.2f;

        Instantiate(hitParticlePrefab, position, Quaternion.identity);
    }

    public void PlayDeathEffect()
    {
        if (dissolvingController != null)
            dissolvingController.StartDissolve();
    }
}