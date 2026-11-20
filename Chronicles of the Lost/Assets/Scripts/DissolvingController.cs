
using UnityEngine;
using System.Collections;
using UnityEngine.VFX;

public class DissolvingController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public VisualEffect VFXGraph;

    [Header("Dissolve Settings")]
    public float dissolveRate = 0.02f;
    public float refreshRate = 0.05f;

    private Material[] dissolveMaterials;
    private bool isDissolving;

    void Start()
    {
        if (VFXGraph != null)
        {
            VFXGraph.Stop();
            VFXGraph.gameObject.SetActive(false);
        }

        if (skinnedMeshRenderer != null)
            dissolveMaterials = skinnedMeshRenderer.materials;
    }

    public void StartDissolve()
    {
        if (isDissolving) return;

        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve()
    {
        isDissolving = true;

        yield return new WaitForSeconds(0.2f);

        if (VFXGraph != null)
        {
            VFXGraph.gameObject.SetActive(true);
            VFXGraph.Play();
        }

        float counter = 0f;

        if (dissolveMaterials != null)
        {
            while (counter < 1f)
            {
                counter = Mathf.Min(counter + dissolveRate, 1f);

                foreach (Material mat in dissolveMaterials)
                {
                    mat.SetFloat("DissolveAmount_", counter);
                }

                yield return new WaitForSeconds(refreshRate);
            }
        }

        Destroy(gameObject, 1f);
    }
}
