
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [Header("Player 1 Only")]
    [SerializeField] private FighterBase player1;
    [SerializeField] private GameObject defeatPanel;

    private bool defeatScreenShown;

    private void Awake()
    {
        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (player1 != null)
            player1.OnFighterDefeated += HandlePlayerDefeated;
    }

    private void OnDisable()
    {
        if (player1 != null)
            player1.OnFighterDefeated -= HandlePlayerDefeated;
    }

    private void HandlePlayerDefeated()
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
    public void ShowDefeatPanel()
{
    if (defeatScreenShown) return;

    defeatScreenShown = true;

    if (defeatPanel != null)
        defeatPanel.SetActive(true);

    Time.timeScale = 0f;
}
}
