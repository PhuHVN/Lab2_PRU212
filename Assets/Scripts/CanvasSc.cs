using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioMangement.instance.PlayStartMusicBackground();
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
}
