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

    [Header("Respawn Settings")]
    public Transform[] respawnPoints;   // isi daftar titik spawn di Inspector
    public Transform playerTransform;   // drag Player object di Inspector

    void Start()
    {
        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            RespawnToNearestPoint();
        }
    }

    void UpdateHealthUI()
    {
        healthBar.value = currentHealth;
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    void RespawnToNearestPoint()
    {
        if (respawnPoints == null || respawnPoints.Length == 0)
        {
            Debug.LogWarning("Respawn point belum diatur!");
            return;
        }

        // Cari spawn point terdekat
        Transform nearest = respawnPoints[0];
        float minDistance = Vector2.Distance(playerTransform.position, nearest.position);

        foreach (Transform point in respawnPoints)
        {
            float dist = Vector2.Distance(playerTransform.position, point.position);
            if (dist < minDistance)
            {
                nearest = point;
                minDistance = dist;
            }
        }

        // Reset knockback
        PlayerMovement pm = playerTransform.GetComponent<PlayerMovement>();
        pm.ResetKnockback();

        // Reset velocity physics
        Rigidbody2D rb = playerTransform.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Pindahkan player ke posisi respawn point persis
        playerTransform.position = nearest.position;

        // Reset health ke full
        currentHealth = maxHealth;
        UpdateHealthUI();

        Debug.Log("Player respawn di: " + nearest.name);
    }
}
