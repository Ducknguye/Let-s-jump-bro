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

        int coin = GameManager.instance.coin;
        int total = GameManager.instance.totalCoin;

        scoreText.text = "Score: " + coin;

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