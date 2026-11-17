using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip punchSwingClip;
    [SerializeField] private AudioClip punchHitClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

 
    public void PlayPunchSwing()
    {
        PlaySound(punchSwingClip);
    }

    public void PlayPunchHit()
    {
        PlaySound(punchHitClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {

            sfxSource.PlayOneShot(clip);
        }
    }
}

