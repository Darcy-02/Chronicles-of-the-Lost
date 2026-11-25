
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath =>
        Path.Combine(Application.persistentDataPath, "GameData.json");

    public static void Save(GameData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("GAME SAVED: " + SavePath);
    }

    public static GameData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found. Creating new game data.");
            return new GameData();
        }

        string json = File.ReadAllText(SavePath);
        GameData data = JsonUtility.FromJson<GameData>(json);

        Debug.Log("GAME DATA LOADED!");

        return data ?? new GameData();
    }
}

