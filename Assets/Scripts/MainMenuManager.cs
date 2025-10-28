using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string playSceneName;

    [Header("Button Animation Settings")]
    [SerializeField] private float clickScale = 0.9f;
    [SerializeField] private float animationDuration = 0.1f;

    /// <summary>
    /// Tombol Play Game
    /// </summary>
    public void PlayGame(Button btn)
    {
        StartCoroutine(AnimateButton(btn, () =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(playSceneName);
        }));
    }

    /// <summary>
    /// Tombol Quit Game
    /// </summary>
    public void QuitGame(Button btn)
    {
        StartCoroutine(AnimateButton(btn, () =>
        {
#if UNITY_EDITOR
            Debug.Log("Quit Game");
#else
            Application.Quit();
#endif
        }));
    }

    /// <summary>
    /// Animasi tombol klik (scale kecil-besar)
    /// </summary>
    private IEnumerator AnimateButton(Button btn, System.Action onComplete)
    {
        Vector3 originalScale = btn.transform.localScale;
        Vector3 targetScale = originalScale * clickScale;

        // Scale down
        yield return ScaleOverTime(btn.transform, originalScale, targetScale, animationDuration);
        // Scale back
        yield return ScaleOverTime(btn.transform, targetScale, originalScale, animationDuration);

        onComplete?.Invoke();
    }

    /// <summary>
    /// Coroutine untuk scale transform secara smooth
    /// </summary>
    private IEnumerator ScaleOverTime(Transform target, Vector3 from, Vector3 to, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            target.localScale = Vector3.Lerp(from, to, timer / duration);
            yield return null;
        }
        target.localScale = to;
    }
}
