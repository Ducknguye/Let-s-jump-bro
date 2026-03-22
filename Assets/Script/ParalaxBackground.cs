using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speedMotion; // toc do di chuyen cua player

    private Vector3 _lastPosition; // luu vi tri cuoi cung cua player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_player = GameObject.FindGameObjectWithTag("Player").transform;
        _lastPosition = _player.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMove = _player.position - _lastPosition; // tinh toan su chuyen dong cua player
        this.transform.position += new Vector3(-deltaMove.x * _speedMotion, 0f, 0f); // di chuyen background theo huong -x
        _lastPosition = _player.position; // cap nhat vi tri cuoi cung
    }
}
