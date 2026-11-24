using System.Collections.Generic;
using UnityEngine;

public class ComboGenerator : MonoBehaviour
{
    public List<AnimationData> GenerateCombo(
        AnimationLibrary library, int comboLength)
    {
        List<AnimationData> combo = new List<AnimationData>();

        if (library == null || library.Animations.Count == 0)
            return combo;

        AnimationData previousMove = null;

        for (int i = 0; i < comboLength; i++)
        {
            List<AnimationData> availableMoves = new List<AnimationData>();

            foreach (AnimationData move in library.Animations)
            {
                if (move == null)
                    continue;

                if (move != previousMove)
                    availableMoves.Add(move);
            }

            if (availableMoves.Count == 0)
                break;

            AnimationData selectedMove = GetWeightedRandomMove(availableMoves);

            combo.Add(selectedMove);
            previousMove = selectedMove;
        }

        return combo;
    }

    private AnimationData GetWeightedRandomMove(List<AnimationData> moves)
    {
        int totalWeight = 0;

        foreach (AnimationData move in moves)
            totalWeight += Mathf.Max(1, move.selectionWeight);

        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (AnimationData move in moves)
        {
            currentWeight += Mathf.Max(1, move.selectionWeight);

            if (randomValue < currentWeight)
                return move;
        }

        return moves[0];
    }
}
