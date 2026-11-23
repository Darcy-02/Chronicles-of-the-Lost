
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private FighterBase player;
    [SerializeField] private GameObject defeatPanel;

    private bool defeatScreenShown;

    private void Awake()
    {
        defeatPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (player != null)
            player.OnFighterDefeated += HandlePlayerDefeated;
    }

    private void OnDisable()
    {
        if (player != null)
            player.OnFighterDefeated -= HandlePlayerDefeated;
    }

    private void HandlePlayerDefeated()
    {
        defeatScreenShown = false;
    }

    public void ShowDefeatPanel()
    {
        if (defeatScreenShown) return;

        defeatScreenShown = true;
        defeatPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
