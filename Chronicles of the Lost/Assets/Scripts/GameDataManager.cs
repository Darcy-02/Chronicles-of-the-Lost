
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [ContextMenu("Test Save Game")]
    public void TestSaveGame()
    {
        CurrentData.totalScore = 100;
        SaveGame();
    }

    [ContextMenu("Check Saved Progress")]
    private void CheckSavedProgress()
    {
        Debug.Log("Completed Levels: " +
            string.Join(", ", CurrentData.completedLevels));

        Debug.Log("Unlocked Levels: " +
            string.Join(", ", CurrentData.unlockedLevels));
    }

    public static GameDataManager Instance { get; private set; }

    public GameData CurrentData { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CurrentData = SaveSystem.Load();
    }

    public void SaveGame()
    {
        SaveSystem.Save(CurrentData);
    }
}
