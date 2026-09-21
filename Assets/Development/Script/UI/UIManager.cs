using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private CoinUI coinUI;
    [SerializeField] private GameOverUI gameOverUI;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    public void InitHealth(float maxHealth)
    {
        healthUI.SetMaxHealth(maxHealth);
    }

    public void RefreshHealth(float currentHealth)
    {
        healthUI.UpdateHealth(currentHealth);
    }

    public void CollectCoin(int amount)
    {
        coinUI.AddCoin(amount);
    }

    public void EndGame()
    {
        gameOverUI.Open();
    }
}
