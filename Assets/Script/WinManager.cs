using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinManager : MonoBehaviour
{
    public static WinManager instance;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private string nextLevelName;

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
        yield return new WaitForSecondsRealtime(3f); // 👈 delay 3s (không bị pause)

        winPanel.SetActive(true);
        Time.timeScale = 0f; // pause game
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