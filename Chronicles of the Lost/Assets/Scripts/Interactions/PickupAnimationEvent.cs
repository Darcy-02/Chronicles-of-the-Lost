
using UnityEngine;

public class PickupAnimationEvent : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;

    public void CollectCurrentItem()
    {
        if (playerInteraction == null) return;

        playerInteraction.SendPickupEvent();
    }
}
