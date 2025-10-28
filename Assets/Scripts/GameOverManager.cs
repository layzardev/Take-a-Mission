using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Game Over")]
    public GameObject gameOverPanel;

    [Header("Respawn")]
    public Transform respawnPoint;
    public GameObject player;
    public HealthBar healthBar;

    private bool isGameOver = false;

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

        ResetPlayerPhysics();

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over!");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGameOver = false;

        ResetPlayer();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        BgmManager.instance?.RestartBGM();
    }

    public void BackHome()
    {
        Time.timeScale = 1f;
        BgmManager.instance?.StopBGM();
        SceneController.instance?.LoadScene("MainMenu");
    }

    private void ResetPlayerPhysics()
    {
        if (player == null) return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        if (pm != null)
        {
            pm.KBCounter = 0f;
            pm.KnockFromRight = false;
        }
    }

    private void ResetPlayer()
    {
        if (player == null || respawnPoint == null) return;

        player.transform.position = respawnPoint.position;
        ResetPlayerPhysics();

        PlayerMovement pm = player.GetComponent<PlayerMovement>();
        pm?.ResetBlinkScreen();

        player.SetActive(true);
        healthBar?.ResetHealth();
    }
}
