
using UnityEngine;

public class LevelCompletionManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int levelNumber = 1;

    [Header("System References")]
    [SerializeField] private LevelObjectiveManager objectiveManager;
    [SerializeField] private LevelRewardManager rewardManager;
    [SerializeField] private LevelUIManager uiManager;

    private void OnEnable()
    {
        if (objectiveManager != null)
            objectiveManager.OnObjectiveCompleted += CompleteLevel;
    }

    private void OnDisable()
    {
        if (objectiveManager != null)
            objectiveManager.OnObjectiveCompleted -= CompleteLevel;
    }

    private void CompleteLevel()
    {
        bool firstCompletion =
            !LevelProgressionManager.IsLevelCompleted(levelNumber);

        LevelProgressionManager.CompleteLevel(levelNumber);


        if (uiManager != null)
        {
            uiManager.ShowVictory(firstCompletion);
        }

        Debug.Log("Level " + levelNumber + " completed and saved!");
    }
}
