
using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{

    public List<int> completedLevels = new List<int>();
    public List<int> unlockedLevels = new List<int>() { 1 };


    public int enemiesDefeated;
    public int totalScore;

    public List<string> inventory = new List<string>();

    // Settings
    public float musicVolume = 1f;
    public float soundVolume = 1f;


    public List<string> unlockedContent = new List<string>();
}

