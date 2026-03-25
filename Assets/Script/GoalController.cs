using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 🔊 phát sound win
            SoundManager.instance.musicSource.Stop();
            SoundManager.instance.PlaySFX(SoundManager.instance.win);

            // 🛑 dừng hoàn toàn player
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.enabled = false;

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;   // dừng di chuyển
                    rb.bodyType = RigidbodyType2D.Kinematic; // tắt vật lý
                }
            }

            // 🎉 debug
            Debug.Log("YOU WIN!");
        }
    }
}