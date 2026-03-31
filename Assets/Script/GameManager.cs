using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coin = 0;
    public TextMeshProUGUI coinText;
    public int totalCoin = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalCoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = "Coin: " + coin;
    }
}