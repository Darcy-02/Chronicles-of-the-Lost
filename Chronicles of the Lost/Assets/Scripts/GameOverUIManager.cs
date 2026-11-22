
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FighterBase player;
    [SerializeField] private GameObject defeatPanel;

    [Header("Settings")]
    [SerializeField] private float animationWaitTime = 2.5f;

    private bool defeatScreenShown;

    private void Awake()
    {
        if (defeatPanel != null)
            defeatPanel.SetActive(false);
    }

    private void OnEnable()
    {
        if (player != null)
            player.OnFighterDefeated += ShowDefeatScreen;
    }

    private void OnDisable()
    {
        if (player != null)
            player.OnFighterDefeated -= ShowDefeatScreen;
    }

    private void ShowDefeatScreen()
    {
        if (defeatScreenShown) return;

        defeatScreenShown = true;
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(animationWaitTime);

        if (defeatPanel != null)
            defeatPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
