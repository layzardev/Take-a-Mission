using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Game Over")]
    public GameObject gameOverPanel;
    public bool isGameOver = false;

    [Header("Respawn")]
    public Transform respawnPoint;
    public GameObject player;

    public HealthBar healthBar; // drag HealthBar

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (respawnPoint == null)
            Debug.LogWarning("RespawnPoint belum di-set!");
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Reset physics & knockback sebelum pause
        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.KBCounter = 0f;
                pm.KnockFromRight = false;
            }
        }

        Time.timeScale = 0f; // pause game

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over!");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.KBCounter = 0f;
                pm.KnockFromRight = false;
            }

            player.SetActive(true);

            // Reset health setelah respawn
            if (healthBar != null)
            {
                healthBar.ResetHealth();
            }
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void BackHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
