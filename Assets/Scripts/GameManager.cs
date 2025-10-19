using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
    }

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (UIController.Instance.pausePanel.activeSelf)
        {
            Time.timeScale = 1f;
            UIController.Instance.pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            UIController.Instance.pausePanel.SetActive(true);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        AudioMangement.instance.PlayGameMusicBackground();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
