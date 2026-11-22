
using UnityEngine;

public class PortalCardPickup1 : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "Pick up Portal Access Card";

    private bool collected;
    private bool beingCollected;

    public void Interact(PlayerInteraction player)
    {
        if (collected || beingCollected) return;

        beingCollected = true;

        Debug.Log("Picking up Portal Access Card...");
    }

    public void CollectCard()
    {
        if (collected || !beingCollected) return;

        collected = true;

        LevelProgressionManager.AddKey(1);

        Debug.Log("Portal Access Card collected!");

        Destroy(gameObject);
    }
}
