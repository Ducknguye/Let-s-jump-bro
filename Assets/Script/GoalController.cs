using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private ParticleSystem left;
    [SerializeField] private ParticleSystem right;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 🔊 sound
            SoundManager.instance.musicSource.Stop();
            SoundManager.instance.PlaySFX(SoundManager.instance.win);

            left.Play();
            right.Play();
            WinManager.instance.OnWin();
        }
    }
}