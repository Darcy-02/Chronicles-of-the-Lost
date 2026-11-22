
using UnityEngine;

public class LevelRewardManager : MonoBehaviour
{
    [Header("Level Reward")]
    [SerializeField] private int keyReward = 1;

    public void AwardLevelReward()
    {
        LevelProgressionManager.AddKey(keyReward);

        Debug.Log("Awarded " + keyReward + " key(s).");
    }
}

