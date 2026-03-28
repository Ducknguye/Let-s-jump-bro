using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    [SerializeField] private GameObject pausePanel;

    private bool _isPaused;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // 👇 bấm ESC để pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (_isPaused) return;

        _isPaused = true;

        pausePanel.SetActive(true);
        Time.timeScale = 0f; // ❗ dừng toàn bộ game (timer cũng dừng)
    }

    public void ResumeGame()
    {
        if (!_isPaused) return;

        _isPaused = false;

        pausePanel.SetActive(false);
        Time.timeScale = 1f; // ▶ chạy lại game
    }

    public void GoHome()
    {
        Time.timeScale = 1f; // ❗ reset trước khi load scene
        SceneManager.LoadScene("Main Menu");
    }
}