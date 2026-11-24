
using UnityEngine;

public class DefeatAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameOverUIManager gameOverUI;
    [SerializeField] private bool isPlayer1;

    public void ShowDefeatPanel()
    {
        if (isPlayer1)
            gameOverUI.ShowDefeatPanel();
        else
            Debug.Log("Opponent defeated. Showing victory flow.");
    }
}
