using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Awake()
    {
        instance = this;
    }

    public void ShowGameOver()
    {
        int coin = GameManager.instance.coin;

        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string key = "BEST_SCORE_" + levelName;

        int best = PlayerPrefs.GetInt(key, 0);

        if (coin > best)
        {
            best = coin;
            PlayerPrefs.SetInt(key, best);
            PlayerPrefs.Save();
        }

        scoreText.text = "Score: " + coin + "\nBest: " + best;

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
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