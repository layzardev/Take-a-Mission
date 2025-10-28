using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition; // tambahin namespace EasyTransition

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [Header("Transition Settings")]
    public TransitionSettings transition; // drag & drop di inspector
    public float startDelay = 0.2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Load scene berikutnya
    public void NextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(SceneUtility.GetScenePathByBuildIndex(nextIndex));
    }

    // Load scene berdasarkan index
    public void LoadScene(int sceneIndex)
    {
        string sceneName = SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        LoadScene(sceneName);
    }

    // Load scene berdasarkan nama
    public void LoadScene(string sceneName)
    {
        if (transition != null)
        {
            TransitionManager.Instance().Transition(sceneName, transition, startDelay);
        }
        else
        {
            // fallback kalau transition belum di-assign
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
