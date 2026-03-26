using UnityEngine;

public class EarnObject : MonoBehaviour
{
    void Start()
    {
        // ✅ Nếu là coin thì mới cộng
        if (CompareTag("Coin"))
        {
            GameManager.instance.AddCoin(10);
            SoundManager.instance.PlaySFX(SoundManager.instance.coinCollect);
        }
    }
}