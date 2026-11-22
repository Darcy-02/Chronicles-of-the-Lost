
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransition : MonoBehaviour
{
    [SerializeField] private string nextScene = "Map2";

    private bool isLoading;

    private void OnTriggerStay(Collider other)
    {
        if (isLoading)
            return;

        if (!other.CompareTag("Player"))
            return;

        // Check whether the player has a Portal Access Card.
        if (LevelProgressionManager.GetKeys() <= 0)
            return;

        // Consume the card before entering.
        if (!LevelProgressionManager.UseKey())
            return;

        isLoading = true;

        Debug.Log("Portal Access Card accepted!");

        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }
}
