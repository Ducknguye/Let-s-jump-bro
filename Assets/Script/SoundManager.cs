using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private float _lastPlayTime = 0f;
    private float _minInterval = 0.05f; // 50ms

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("SFX")]
    public AudioClip breakBrick;
    public AudioClip coinCollect;
    public AudioClip die;
    public AudioClip enemyDie;
    public AudioClip itemCollect;
    public AudioClip jump;
    public AudioClip land;
    public AudioClip win;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        // 🚫 chặn spam quá nhanh
        if (Time.time - _lastPlayTime < _minInterval) return;

        sfxSource.PlayOneShot(clip);
        _lastPlayTime = Time.time;
    }
}