
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

        if (GameDataManager.Instance != null)
        {
            GameData data = GameDataManager.Instance.CurrentData;

            if (!data.completedLevels.Contains(levelNumber))
                data.completedLevels.Add(levelNumber);

            if (levelNumber < 5 &&
                !data.unlockedLevels.Contains(levelNumber + 1))
            {
                data.unlockedLevels.Add(levelNumber + 1);
            }

            if (firstCompletion)
                data.enemiesDefeated++;

            GameDataManager.Instance.SaveGame();
        }

        if (uiManager != null)
            uiManager.ShowVictory(firstCompletion);

        Debug.Log("Level " + levelNumber + " completed and saved!");
    }

}
