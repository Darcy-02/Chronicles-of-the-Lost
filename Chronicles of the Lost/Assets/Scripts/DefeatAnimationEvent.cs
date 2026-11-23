
using UnityEngine;

public class DefeatAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameOverUIManager gameOverUI;

    public void ShowDefeatPanel()
    {
        gameOverUI.ShowDefeatPanel();
    }
}
