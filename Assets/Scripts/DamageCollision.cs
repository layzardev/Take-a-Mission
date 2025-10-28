using UnityEngine;

public class DamageCollision : MonoBehaviour
{
    public int damage = 1;                       // jumlah damage
    public HealthBar playerHealth;               // drag HealthBar Player
    public PlayerMovement playerMovement;        // drag PlayerMovement Player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ambil komponen blink dari player
            PlayerBlinkInvulnerable blink = collision.gameObject.GetComponent<PlayerBlinkInvulnerable>();

            // hanya damage kalau player tidak kebal
            if (blink != null && !blink.IsInvulnerable())
            {
                // kurangi darah
                playerHealth.TakeDamage(damage);

                // knockback player
                playerMovement.KBCounter = playerMovement.KBTotalTime;
                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.KnockFromRight = true;
                }
                else
                {
                    playerMovement.KnockFromRight = false;
                }

                // aktifkan efek blink invulnerable
                blink.StartBlink();
            }
        }
    }
}
