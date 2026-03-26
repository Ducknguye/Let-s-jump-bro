using System.Collections;
using UnityEngine;

public class SpikeUpTrap : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform spike;

    [Header("Position")]
    [SerializeField] private float upHeight = 2f;
    private Vector3 _startPos;
    private Vector3 _upPos;

    [Header("Timing")]
    [SerializeField] private float delayBeforeUp = 0.3f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float stayTime = 1f;

    private bool _isRunning;

    private void Start()
    {
        _startPos = spike.position;
        _upPos = _startPos + Vector3.up * upHeight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isRunning) return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SpikeRoutine());
        }
    }

    private IEnumerator SpikeRoutine()
    {
        _isRunning = true;

        yield return new WaitForSeconds(delayBeforeUp);

        // Đâm lên
        while (Vector3.Distance(spike.position, _upPos) > 0.05f)
        {
            spike.position = Vector3.MoveTowards(spike.position, _upPos, speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(stayTime);

        // Rút xuống
        while (Vector3.Distance(spike.position, _startPos) > 0.05f)
        {
            spike.position = Vector3.MoveTowards(spike.position, _startPos, speed * Time.deltaTime);
            yield return null;
        }

        _isRunning = false;
    }
}