
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

    private Coroutine countdownCoroutine;

    private void Start()
    {
        Debug.Log("MATCH MANAGER: Start called.");
        StartNewMatch();
    }


    public void StartNewMatch()
    {
        Debug.Log("MATCH MANAGER: StartNewMatch called.");

        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            Debug.Log("MATCH MANAGER: Previous countdown stopped.");
        }

        Debug.Log("MATCH MANAGER: Starting countdown coroutine.");

        countdownCoroutine = StartCoroutine(StartMatch());
    }




    public void SetNewOpponent(FighterBase newOpponent)
    {
        Debug.Log("MATCH MANAGER: Assigning new opponent.");

        player2 = newOpponent;

        Debug.Log("MATCH MANAGER: New opponent assigned: " +
                  (player2 != null ? player2.name : "NULL"));
    }

    private IEnumerator StartMatch()
    {
        Debug.Log("COUNTDOWN: Coroutine started.");

        if (player1 == null || player2 == null || countdownText == null)
        {
            Debug.LogError("COUNTDOWN ERROR: A reference is missing!");
            yield break;
        }

        Debug.Log("COUNTDOWN: Pausing both fighters.");

        player1.SetCanFight(false);
        player2.SetCanFight(false);

        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            Debug.Log("COUNTDOWN: " + i);

            yield return new WaitForSeconds(countdownDuration);
        }

        countdownText.text = "FIGHT!";
        Debug.Log("COUNTDOWN: FIGHT!");

        player1.SetCanFight(true);
        player2.SetCanFight(true);

        yield return new WaitForSeconds(fightTextDuration);

        countdownText.gameObject.SetActive(false);

        Debug.Log("COUNTDOWN: Finished.");

        countdownCoroutine = null;
    }
}
