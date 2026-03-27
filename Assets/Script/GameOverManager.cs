using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        instance = this;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // cực kỳ quan trọng
        SceneManager.LoadScene("Main Menu"); // đổi thành tên scene của bạn
    }

}