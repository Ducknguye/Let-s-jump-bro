using UnityEngine;
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

        // ✅ chặn theo từng clip, không phải global
        if (_lastPlayTimes.ContainsKey(clip))
        {
            if (Time.time - _lastPlayTimes[clip] < _minInterval)
                return;
        }

        sfxSource.PlayOneShot(clip);
        _lastPlayTimes[clip] = Time.time;
    }
}