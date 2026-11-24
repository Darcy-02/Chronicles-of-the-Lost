using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboExecutor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private ComboGenerator comboGenerator;
    [SerializeField] private AnimationLibrary animationLibrary;

    [Header("Combo Settings")]
    [SerializeField] private int comboLength = 3;
    [SerializeField] private float transitionDuration = 0.1f;

    [SerializeField] private FighterCombat fighterCombat;

    private List<AnimationData> currentCombo;

    private void Awake()
    {
        if (fighterCombat == null)
            fighterCombat = GetComponent<FighterCombat>();
    }

    public void GenerateAndPlayCombo()
    {
        if (animator == null || comboGenerator == null || animationLibrary == null)
            return;

        currentCombo = comboGenerator.GenerateCombo(
            animationLibrary, comboLength);

        Debug.Log("COMBO GENERATED: " + currentCombo.Count);

        foreach (AnimationData move in currentCombo)
        {
            Debug.Log("MOVE: " + move.animationName +
                      " | STATE: " + move.animatorStateName);
        }

        StopAllCoroutines();
        StartCoroutine(PlayCombo());
    }

    private IEnumerator PlayCombo()
    {
        foreach (AnimationData move in currentCombo)
        {
            if (move == null)
                continue;

            if (move.category == MoveCategory.Punch)
            {
                fighterCombat?.Punch();
            }
            else if (move.category == MoveCategory.Kick)
            {
                fighterCombat?.Kick();
            }
            else if (move.category == MoveCategory.Block)
            {
                animator.CrossFade(move.animatorStateName, transitionDuration);
            }
            else
            {
                animator.CrossFade(move.animatorStateName, transitionDuration);
            }

            yield return new WaitForSeconds(move.duration);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C KEY DETECTED!");
           
            GenerateAndPlayCombo();
        }
    }
}