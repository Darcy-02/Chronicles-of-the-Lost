
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileActionButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    public enum ActionType
    {
        Punch,
        Kick,
        Block,
        Combo
    }

    [SerializeField] private PlayerController player;
    [SerializeField] private ActionType action;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (action)
        {
            case ActionType.Punch:
                player.MobilePunch();
                break;

            case ActionType.Kick:
                player.MobileKick();
                break;

            case ActionType.Block:
                player.MobileBlock();
                break;
            case ActionType.Combo:
                player.MobileCombo();
                break;
        }


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (action == ActionType.Block)
            player.MobileStopBlock();
    }
}
