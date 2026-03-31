using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager instance;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private string nextLevelName;

    [SerializeField] private TextMeshProUGUI scoreText;

    private bool _isWin;

    private void Awake()
    {
        instance = this;
    }

    public void OnWin()
    {
        if (_isWin) return;
        _isWin = true;

        StartCoroutine(ShowWinPanel());
    }

    private IEnumerator ShowWinPanel()
    {
        yield return new WaitForSecondsRealtime(3f);

        // Lấy điểm hiện tại
        int coin = GameManager.instance.coin;

        // Lấy tên scene hiện tại
        string levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        string key = "BEST_SCORE_" + levelName;

        // Lấy best score
        int best = PlayerPrefs.GetInt(key, 0);

        bool isNewRecord = false;

        // Nếu phá kỷ lục
        if (coin > best)
        {
            best = coin;
            PlayerPrefs.SetInt(key, best);
            PlayerPrefs.Save();
            isNewRecord = true;
        }

        // Hiển thị UI
        scoreText.text = "Score: " + coin + "\nBest: " + best;

        if (isNewRecord)
        {
            scoreText.text += "\nNEW BEST!";
        }

        // Hiện panel + pause
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelName);
    }
}