using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Mục tiêu cần theo đuổi")]
    public Transform player;

    [Header("Khoảng cách Camera")]
    // Trục Z để -10 để camera lùi lại nhìn thấy cảnh, nếu để 0 sẽ bị kẹt vào nhân vật
    public Vector3 offset = new Vector3(0, 0, -10f); 

    [Header("Độ mượt (Càng nhỏ càng mượt)")]
    public float smoothSpeed = 0.125f;

    // Dùng LateUpdate thay vì Update để tránh hiện tượng giật lag hình ảnh
    void LateUpdate()
    {
        if (player != null)
        {
            // Tính toán vị trí mới
            Vector3 desiredPosition = player.position + offset;
            
            // Di chuyển mượt mà từ vị trí hiện tại tới vị trí mới
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            transform.position = smoothedPosition;
        }
    }
}