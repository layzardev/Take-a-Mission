using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;

    [Header("Audio Clips")]
    public AudioClip mainMenuClip;
    public AudioClip inGameClip;

    private AudioSource audioSource;
    private string currentScene = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;

        if (currentScene == "MainMenu" && mainMenuClip != null)
        {
            PlayClip(mainMenuClip);
        }
        else if (currentScene.Contains("Level") && inGameClip != null)
        {
            PlayClip(inGameClip);
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void PlayClip(AudioClip clip)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
            return; // sudah main, jangan restart

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void RestartBGM()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Stop();
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}
