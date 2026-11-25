
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        player.SetMobileMovement(direction, 0f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.SetMobileMovement(0f, 0f);
    }
}
