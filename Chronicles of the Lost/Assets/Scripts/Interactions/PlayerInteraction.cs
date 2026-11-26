
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 2f;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text interactionPrompt;

    private IInteractable currentInteractable;
    private bool isInteracting;

    private void Start()
    {
        if (interactionPrompt != null)
            interactionPrompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Interact();
    }

    public void MobileInteract()
    {
        Interact();
    }

    private void Interact()
    {
        if (currentInteractable == null || isInteracting)
            return;

        isInteracting = true;

        if (animator != null)
            animator.SetTrigger("Pickup");

        currentInteractable.Interact(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable == null) return;

        currentInteractable = interactable;

        if (interactionPrompt != null)
        {
            interactionPrompt.text =
                "[SPACE] " + interactable.InteractionPrompt;

            interactionPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable == null) return;

        if (currentInteractable == interactable)
        {
            currentInteractable = null;
            isInteracting = false;

            if (interactionPrompt != null)
                interactionPrompt.gameObject.SetActive(false);
        }
    }

    public void FinishInteraction()
    {
        isInteracting = false;
    }

    public void SetCurrentInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    public void SendPickupEvent()
    {
        if (currentInteractable is PortalCardPickup1 card)
            card.CollectCard();

        FinishInteraction();
    }
}
