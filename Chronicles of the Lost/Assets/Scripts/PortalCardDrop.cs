
using UnityEngine;

public class PortalCardDrop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FighterBase guardian;
    [SerializeField] private GameObject cardPrefab;

    [Header("Drop Settings")]
    [SerializeField] private float heightOffset = 1f;

    private bool cardDropped;

    private void OnEnable()
    {
        if (guardian != null)
            guardian.OnFighterDefeated += DropCard;
    }

    private void OnDisable()
    {
        if (guardian != null)
            guardian.OnFighterDefeated -= DropCard;
    }

    private void DropCard()
    {
        if (cardDropped || cardPrefab == null) return;

        cardDropped = true;

        Vector3 dropPosition =
            guardian.transform.position + Vector3.up * heightOffset;

        Instantiate(cardPrefab, dropPosition, Quaternion.identity);

        Debug.Log("Portal Access Card dropped!");
    }
}
