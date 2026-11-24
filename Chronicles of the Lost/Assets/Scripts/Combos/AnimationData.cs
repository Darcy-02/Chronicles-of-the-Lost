
using UnityEngine;

public enum MoveCategory
{
    Punch,
    Kick,
    Block,
    Special
}

[System.Serializable]
public class AnimationData
{
    [Header("Animation Information")]
    public string animationName;

    public string animatorStateName;

    public AnimationClip animationClip;

    public MoveCategory category;

    [Header("Combo Settings")]
    [Range(1, 10)]
    public int selectionWeight = 5;

    [Header("Timing")]
    public float duration = 1f;
}
