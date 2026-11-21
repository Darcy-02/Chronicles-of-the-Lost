using UnityEngine;

public class FighterAudio : MonoBehaviour
{
    public void PlayPunch()
    {
        AudioManager.Instance?.PlayPunch();
    }

    public void PlayKick()
    {
        AudioManager.Instance?.PlayKick();
    }

    public void PlayHit()
    {
        AudioManager.Instance?.PlayHit();
    }

    public void PlayBlock()
    {
        AudioManager.Instance?.PlayBlock();
    }

    public void PlayDeath()
    {
        AudioManager.Instance?.PlayDeath();
    }
}