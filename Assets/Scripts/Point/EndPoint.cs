using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public GameObject winUi;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            winUi.SetActive(true);
        }
    }

    public void Restart(string levelName)
    {
        Time.timeScale = 1f;
        BgmManager.instance?.RestartBGM();

        // Panggil transisi dari SceneController
        SceneController.instance.LoadScene(levelName);
    }

    public void BackHome()
    {
        Time.timeScale = 1f;
        BgmManager.instance?.StopBGM();

        SceneController.instance.LoadScene("MainMenu");
    }
}
