
using UnityEngine;
using TMPro;

public class LevelUIManager : MonoBehaviour
{
    [Header("Objective UI")]
    [SerializeField] private TMP_Text objectiveText;

    [Header("Victory UI")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text rewardText;

    [Header("Objective Manager")]
    [SerializeField] private LevelObjectiveManager objectiveManager;

    private void Awake()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (objectiveText != null)
            objectiveText.text = "Objective: Defeat the Guardian";
    }

    private void OnEnable()
    {
        if (objectiveManager != null)
            objectiveManager.OnObjectiveCompleted += ShowObjectiveCompleted;
    }

    private void OnDisable()
    {
        if (objectiveManager != null)
            objectiveManager.OnObjectiveCompleted -= ShowObjectiveCompleted;
    }

    private void ShowObjectiveCompleted()
    {
        if (objectiveText != null)
            objectiveText.text = "Objective Completed!";
    }

    public void ShowVictory(bool receivedKey)
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (rewardText != null)
        {
            rewardText.text = receivedKey
                ? "You received a Portal Access Card!"
                : "Level already completed. No new key awarded.";
        }
    }

    public void HideVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }
}
