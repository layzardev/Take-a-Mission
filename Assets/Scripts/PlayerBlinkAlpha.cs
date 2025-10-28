using UnityEngine;

public class PlayerBlinkInvulnerable : MonoBehaviour
{
    public float blinkDuration = 1f;
    public float blinkInterval = 0.1f;
    private SpriteRenderer sr;
    private TrailRenderer tr; // Tambahkan TrailRenderer
    private bool isInvulnerable = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>(); // Ambil TrailRenderer jika ada
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }

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

        // Simpan warna awal TrailRenderer agar bisa dikembalikan
        Color trColor = tr != null ? tr.startColor : Color.white;

        while (elapsed < blinkDuration)
        {
            if (transparent)
            {
                sr.color = new Color(1f, 1f, 1f, 1f);
                if (tr != null)
                    tr.startColor = trColor; // kembalikan warna awal
            }
            else
            {
                sr.color = new Color(1f, 1f, 1f, 0.3f);
                if (tr != null)
                {
                    Color c = trColor;
                    c.a = 0.3f;
                    tr.startColor = c; // ubah alpha Trail
                }
            }

            transparent = !transparent;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        sr.color = new Color(1f, 1f, 1f, 1f);
        if (tr != null)
            tr.startColor = trColor; // pastikan kembalikan warna

        isInvulnerable = false;
    }
}
