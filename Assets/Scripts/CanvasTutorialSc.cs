using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasTutorialSc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioMangement.instance.PlayStartMusicBackground();
    }

    public void onClickQuitEvent()
    {
        SceneManager.LoadSceneAsync(0);
        AudioMangement.instance.StopMusic();
    }
}
