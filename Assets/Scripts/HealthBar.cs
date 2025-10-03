using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Slider healthBar;
    public TMP_Text healthText;

    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Game Over Manager")]
    public GameOverManager gameOverManager; // drag GameOverManager di Inspector

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
            else
            {
                Debug.LogWarning("GameOverManager belum di-set! Reset health langsung.");
                ResetHealth();
            }
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;

        if (healthText != null)
            healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    // Method ini bisa dipanggil dari tombol Restart di GameOverManager
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log("Health sudah full kembali.");
    }
}
