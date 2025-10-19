using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSc : MonoBehaviour
{

    public TMP_Text highScoreText;
    private int highScore;
    void Start()
    {
        Time.timeScale = 1f;
        if (AudioMangement.instance != null)
        {
            AudioMangement.instance.PlayStartMusicBackground();
        }
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickPlayEvent()
    {
        AudioMangement.instance.PlayButtonClick();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        AudioMangement.instance.StopMusic();
        AudioMangement.instance.PlayGameMusicBackground();
    }
    public void onClickQuitEvent()
    {
        AudioMangement.instance.PlayButtonClick();
        Application.Quit();
    }

    public void onClickTutorialEvent()
    {
        AudioMangement.instance.PlayButtonClick();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 2);
        AudioMangement.instance.StopMusic();
        AudioMangement.instance.PlayGameMusicBackground();
    }
}
