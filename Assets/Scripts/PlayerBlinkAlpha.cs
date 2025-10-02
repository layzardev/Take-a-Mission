using UnityEngine;

public class PlayerBlinkInvulnerable : MonoBehaviour
{
    public float blinkDuration = 1f;
    public float blinkInterval = 0.1f;
    private SpriteRenderer sr;
    private bool isInvulnerable = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // ? biar bisa dicek dari luar
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

    // ? biar bisa dipanggil dari Damage.cs
    public void StartBlink()
    {
        if (!isInvulnerable)
        {
            StartCoroutine(BlinkAlpha());
        }
    }

    private System.Collections.IEnumerator BlinkAlpha()
    {
        isInvulnerable = true;

        float elapsed = 0f;
        bool transparent = false;

        while (elapsed < blinkDuration)
        {
            if (transparent)
                sr.color = new Color(1f, 1f, 1f, 1f);
            else
                sr.color = new Color(1f, 1f, 1f, 0.3f);

            transparent = !transparent;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        sr.color = new Color(1f, 1f, 1f, 1f);
        isInvulnerable = false;
    }
}
