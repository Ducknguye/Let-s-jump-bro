using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private Dictionary<AudioClip, float> _lastPlayTimes = new Dictionary<AudioClip, float>();
    [SerializeField] private float _minInterval = 0.05f;

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

            // 🔥 lắng nghe khi load scene
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 🔥 đảm bảo luôn có nhạc khi vào scene mới
        if (!musicSource.isPlaying)
        {
            PlayMusic(backgroundMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        // tránh play lại nếu đang chạy đúng nhạc
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        if (_lastPlayTimes.ContainsKey(clip))
        {
            if (Time.time - _lastPlayTimes[clip] < _minInterval)
                return;
        }

        sfxSource.PlayOneShot(clip);
        _lastPlayTimes[clip] = Time.time;
    }
}