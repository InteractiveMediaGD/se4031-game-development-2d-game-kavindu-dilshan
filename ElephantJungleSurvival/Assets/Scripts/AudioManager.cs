using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources (Auto-Generated)")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Required Sound Effects (Drag files here!)")]
    public AudioClip shootClip;
    public AudioClip hitClip;
    public AudioClip impactClip;
    public AudioClip gameOverClip;
    public AudioClip clickClip;
    
    [Header("Bonus Sound Effects")]
    public AudioClip enemyHitPlayerClip;
    public AudioClip healthPickupClip;

    [Header("Background Music")]
    public AudioClip bgmClip;

    private void Awake()
    {
        // 1. Implement Singleton pattern (DontDestroyOnLoad)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 🌟 FOOLPROOFING: Automatically create the AudioSources for you if you forget to add them!
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true; // Ensure music loops continuously
                musicSource.volume = 0.5f; // Set a nice default music volume
            }
            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            // If another AudioManager exists in this scene, destroy this duplicate
            Destroy(gameObject);
        }
    }

    // --- Required Functions ---

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            // PlayOneShot allows multiple SFX (like machine gun shooting) to overlap cleanly!
            sfxSource.PlayOneShot(clip); 
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // --- Developer Shortcuts for Script Integration ---
    // These make triggering sounds from other scripts incredibly easy!

    public void PlayShootSound() => PlaySFX(shootClip);
    public void PlayHitSound() => PlaySFX(hitClip);
    public void PlayImpactSound() => PlaySFX(impactClip);
    public void PlayClickSound() => PlaySFX(clickClip);
    public void PlayEnemyHitPlayerSound() => PlaySFX(enemyHitPlayerClip);
    public void PlayHealthPickupSound() => PlaySFX(healthPickupClip);

    public void PlayGameOverSound()
    {
        StopMusic(); // Stop music when player dies (game over)
        PlaySFX(gameOverClip);
    }
}
