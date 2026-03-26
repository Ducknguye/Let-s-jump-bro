using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody2D trapRb;

    [Header("Timing")]
    [SerializeField] private float fallDelay = 0.2f;        // delay trước khi rơi
    [SerializeField] private float resetDelay = 2f;         // thời gian chờ reset hoặc destroy

    [Header("Trigger Limit")]
    [SerializeField] private int maxTriggerCount = 3;       // số lần kích hoạt tối đa
    private int currentTriggerCount = 0;

    private bool triggered;

    private Vector3 startPos;

    private void Start()
    {
        // lưu vị trí ban đầu để reset
        startPos = trapRb.transform.position;

        // đảm bảo trạng thái ban đầu đúng
        trapRb.bodyType = RigidbodyType2D.Kinematic;
        trapRb.gravityScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (!collision.CompareTag("Player")) return;

        // nếu đã hết lượt thì không làm gì nữa
        if (currentTriggerCount >= maxTriggerCount) return;

        triggered = true;
        currentTriggerCount++;

        Debug.Log($"Trap triggered: {currentTriggerCount}/{maxTriggerCount}");

        Invoke(nameof(Drop), fallDelay);
    }

    private void Drop()
    {
        // bật physics để rơi
        trapRb.bodyType = RigidbodyType2D.Dynamic;
        trapRb.gravityScale = 4f;

        // nếu đã dùng hết lượt → hủy sau khi rơi
        if (currentTriggerCount >= maxTriggerCount)
        {
            Invoke(nameof(DestroyTrap), resetDelay);
        }
        else
        {
            // chưa hết lượt → reset lại để dùng tiếp
            Invoke(nameof(ResetTrap), resetDelay);
        }
    }

    private void ResetTrap()
    {
        // reset vị trí + trạng thái
        trapRb.linearVelocity = Vector2.zero;
        trapRb.bodyType = RigidbodyType2D.Kinematic;
        trapRb.gravityScale = 0;

        trapRb.transform.position = startPos;

        triggered = false;
    }

    private void DestroyTrap()
    {
        // lấy collider của trap_0 (object cha)
        Collider2D col = trapRb.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // optional: tắt luôn sprite
        SpriteRenderer sr = trapRb.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = false;
        }

        // destroy sau chút để tránh lỗi physics
        Destroy(trapRb.gameObject, 0.1f);
    }
}