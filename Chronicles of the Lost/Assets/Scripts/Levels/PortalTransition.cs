using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransition : MonoBehaviour
{
    [SerializeField] private string nextScene = "Map2";
    [SerializeField] private FighterBase opponent;

    private bool portalUnlocked;
    private bool isLoading;

    private void OnEnable()
    {
        if (opponent != null)
            opponent.OnFighterDefeated += UnlockPortal;
    }

    private void OnDisable()
    {
        if (opponent != null)
            opponent.OnFighterDefeated -= UnlockPortal;
    }

    private void UnlockPortal()
    {
        portalUnlocked = true;
        Debug.Log("Portal unlocked!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (isLoading || !portalUnlocked)
            return;

        if (!other.CompareTag("Player"))
            return;

        isLoading = true;
        SceneManager.LoadScene(nextScene);
    }
}