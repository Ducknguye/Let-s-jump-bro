using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int coin = 0;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
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