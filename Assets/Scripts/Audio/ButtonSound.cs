using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;   // suara klik
    private static AudioSource audioSource;

    void Awake()
    {
        // Cari / buat AudioSource global
        if (audioSource == null)
        {
            GameObject go = new GameObject("UI_SoundPlayer");
            audioSource = go.AddComponent<AudioSource>();
            DontDestroyOnLoad(go);  // biar tetap ada saat pindah scene
        }

        // Tambah listener ke button ini
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
