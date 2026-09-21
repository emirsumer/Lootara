using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private int _coinCount;
    private void Start()
    {
        coinText.text = _coinCount.ToString();
    }
    public void AddCoin(int amount)
    {
        _coinCount += amount;
        coinText.text = _coinCount.ToString();
    }
}
