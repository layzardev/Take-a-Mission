using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public HealthBar playerHealth;
    public int damage;

    public PlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.ApplyKnockback(true);  // mental ke kiri
            }
            else
            {
                playerMovement.ApplyKnockback(false); // mental ke kanan
            }

            playerHealth.TakeDamage(damage);
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerHealth.TakeDamage(damage);
    //    }
    //}
}
