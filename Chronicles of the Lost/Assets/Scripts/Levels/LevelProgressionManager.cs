
using System.IO;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    public int highestUnlockedLevel = 1;
    public int completedLevels = 0;
    public int keys = 0;
}

public static class LevelProgressionManager
{
    private const int TOTAL_LEVELS = 5;

    private static string SavePath =>
        Path.Combine(Application.persistentDataPath,
        "chronicles_progress.json");

    private static ProgressData progress;

    private static void Load()
    {
        if (progress != null) return;

        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);

            progress = JsonUtility.FromJson<ProgressData>(json);
        }

        if (progress == null)
            progress = new ProgressData();
    }

    private static void Save()
    {
        string json = JsonUtility.ToJson(progress, true);
        File.WriteAllText(SavePath, json);
    }

    public static void CompleteLevel(int level)
    {
        Load();

        if (level < 1 || level > TOTAL_LEVELS) return;

        if (level > progress.completedLevels)
            progress.completedLevels = level;

        progress.highestUnlockedLevel =
            Mathf.Max(
                progress.highestUnlockedLevel,
                Mathf.Min(level + 1, TOTAL_LEVELS)
            );

        Save();
    }

    public static void AddKey(int amount)
    {
        Load();

        if (amount <= 0) return;

        progress.keys += amount;
        Save();
    }

    public static bool IsLevelUnlocked(int level)
    {
        Load();
        return level >= 1 &&
               level <= progress.highestUnlockedLevel;
    }

    public static bool IsLevelCompleted(int level)
    {
        Load();
        return level >= 1 &&
               level <= progress.completedLevels;
    }

    public static int GetKeys()
    {
        Load();
        return progress.keys;
    }

    public static bool UseKey()
    {
        Load();

        if (progress.keys <= 0)
            return false;

        progress.keys--;
        Save();

        return true;
    }
}
