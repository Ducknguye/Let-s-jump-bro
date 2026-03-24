using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static UnityAction<float> EvtMove;
    public static UnityAction EvtJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnPlayerMoveLeft()
    {
        EvtMove?.Invoke(-1f);
    }

    public void OnPlayerMoveRight()
    {
        EvtMove?.Invoke(1f);
    }

    public void OnPlayerStop()
    {
        EvtMove?.Invoke(0);
    }

    public void OnPlayerJump()
    {
        EvtJump?.Invoke();
    }
}
