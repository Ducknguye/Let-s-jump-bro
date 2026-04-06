using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject confirmQuitPanel;

    // Khi bấm nút Quit
    public void ShowQuitConfirm()
    {
        confirmQuitPanel.SetActive(true);
    }

    // Khi bấm YES
    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Khi bấm NO
    public void CancelQuit()
    {
        confirmQuitPanel.SetActive(false);
    }
}