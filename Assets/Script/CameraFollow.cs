using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // player
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _minX; // gioi han trai
    [SerializeField] private float _maxX; // gioi han phai
    [SerializeField] private float _minY; // gioi han duoi

    private float _camWidth, _camHeight;

    void Start()
    {
        _camHeight = Camera.main.orthographicSize;
        _camWidth = _camHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.position + _offset;

        float clampedX = Mathf.Clamp(desiredPosition.x, _minX + _camWidth, _maxX - _camWidth);

        // giữ nguyên Y hiện tại
        float fixedY = transform.position.y;

        Vector3 targetPos = new Vector3(clampedX, fixedY, -10f);

        transform.position = Vector3.Lerp(transform.position, targetPos, _speed);
    }
}