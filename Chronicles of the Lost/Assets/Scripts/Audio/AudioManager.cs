
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;

    [Header("Combat Sounds")]
    [SerializeField] private AudioClip punchSound;
    [SerializeField] private AudioClip kickSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip blockSound;
    [SerializeField] private AudioClip deathSound;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayPunch()
    {
        Debug.Log("PUNCH SOUND REQUESTED");
        PlaySound(punchSound);
    }

    public void PlayKick()
    {
        PlaySound(kickSound);
    }

    public void PlayHit()
    {
        PlaySound(hitSound);
    }

    public void PlayBlock()
    {
        PlaySound(blockSound);
    }

    public void PlayDeath()
    {
        PlaySound(deathSound);
    }


    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("Audio clip is missing!");
            return;
        }

        if (sfxSource == null)
        {
            Debug.LogError("SFX Source is missing!");
            return;
        }

        Debug.Log("PLAYING SOUND: " + clip.name);
        sfxSource.PlayOneShot(clip);
    }
}

