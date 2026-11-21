
using System.Collections;
using UnityEngine;
using TMPro;

public class MatchManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text countdownText;

    [Header("Fighters")]
    [SerializeField] private FighterBase player1;
    [SerializeField] private FighterBase player2;

    [Header("Countdown Settings")]
    [SerializeField] private float countdownDuration = 1f;
    [SerializeField] private float fightTextDuration = 1f;

    private void Start()
    {
        StartCoroutine(StartMatch());
    }

    private IEnumerator StartMatch()
    {
        // Keep both fighter components enabled.
        // Only pause their ability to fight.
        player1.SetCanFight(false);
        player2.SetCanFight(false);

        countdownText.gameObject.SetActive(true);

        // Countdown: 3, 2, 1
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(countdownDuration);
        }

        // Start the fight
        countdownText.text = "FIGHT!";

        player1.SetCanFight(true);
        player2.SetCanFight(true);

        yield return new WaitForSeconds(fightTextDuration);

        countdownText.gameObject.SetActive(false);
    }
}

