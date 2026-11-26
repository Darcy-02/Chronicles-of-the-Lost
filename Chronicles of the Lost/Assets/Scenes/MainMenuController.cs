
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "Map1";
    [SerializeField] private string continueSceneName = "Map1";

    [Header("UI Panels")]
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Leaderboard")]
    [SerializeField] private LeaderboardAPI leaderboardAPI;
    [SerializeField] private TMP_Text leaderboardText;

    private void OnEnable()
    {
        if (leaderboardAPI != null)
            leaderboardAPI.OnLeaderboardLoaded += DisplayLeaderboard;
    }

    private void OnDisable()
    {
        if (leaderboardAPI != null)
            leaderboardAPI.OnLeaderboardLoaded -= DisplayLeaderboard;
    }

    private void Start()
    {
        ClosePanels();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueSceneName);
    }

    public void OpenLeaderboard()
    {
        ClosePanels();

        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(true);

        if (leaderboardText != null)
            leaderboardText.text = "Loading leaderboard...";

        if (leaderboardAPI != null)
            leaderboardAPI.GetLeaderboard();
        else if (leaderboardText != null)
            leaderboardText.text = "Leaderboard service unavailable.";
    }

    private void DisplayLeaderboard(string data)
    {
        if (leaderboardText != null)
            leaderboardText.text = data;
    }

    public void OpenInstructions()
    {
        ClosePanels();

        if (instructionsPanel != null)
            instructionsPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        ClosePanels();

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void ClosePanels()
    {
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(false);

        if (instructionsPanel != null)
            instructionsPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        ClosePanels();
    }

    public void ExitGame()
    {
        Debug.Log("Game exited!");
        Application.Quit();
    }
}
