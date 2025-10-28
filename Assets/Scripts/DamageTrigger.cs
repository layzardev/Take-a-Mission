using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageTrigger : MonoBehaviour
{
    public int damage = 1;               // jumlah damage
    public HealthBar playerHealth;       // drag HealthBar Player
    public Image damageScreen;           // drag panel UI merah di canvas

    public float screenBlinkDuration = 0.2f; // durasi layar blink

    private bool isBlinking = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ambil komponen PlayerBlinkInvulnerable
            PlayerBlinkInvulnerable blink = other.GetComponent<PlayerBlinkInvulnerable>();

            // hanya damage kalau player tidak kebal
            if (blink != null && !blink.IsInvulnerable())
            {
                // kurangi darah player
                playerHealth.TakeDamage(damage);

                // efek blink player
                blink.StartBlink();

                // efek blink layar
                if (damageScreen != null)
                    StartCoroutine(ScreenBlink());
            }
        }
    }

    private IEnumerator ScreenBlink()
    {
        if (isBlinking) yield break;  // cegah menumpuk
        isBlinking = true;

        damageScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(screenBlinkDuration);
        damageScreen.gameObject.SetActive(false);

        isBlinking = false;
    }
}
