
using System;
using UnityEngine;

public class LevelObjectiveManager : MonoBehaviour
{
    [Header("Guardian")]
    [SerializeField] private FighterBase guardian;

    public event Action OnObjectiveCompleted;

    private bool objectiveCompleted;

    private void OnEnable()
    {
        if (guardian != null)
            guardian.OnFighterDefeated += CompleteObjective;
    }

    private void OnDisable()
    {
        if (guardian != null)
            guardian.OnFighterDefeated -= CompleteObjective;
    }

    private void CompleteObjective()
    {
        if (objectiveCompleted) return;

        objectiveCompleted = true;

        Debug.Log("Level 1 objective completed!");

        OnObjectiveCompleted?.Invoke();
    }
}
