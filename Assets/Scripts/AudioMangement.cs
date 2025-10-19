using UnityEngine;

public class AudioMangement : MonoBehaviour
{
    public static AudioMangement instance;
    private AudioSource audioM;
    [Header("Audio Clips")]
    public AudioClip buttonClick;
    public AudioClip coinCollect;
    public AudioClip jump;
    public AudioClip gameOver;
    public AudioClip hit;
    public AudioClip StartMusicBackground;
    public AudioClip GameMusicBackground;
    public AudioClip monsterCollision;
    public AudioClip itemCollection;
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
        audioM = GetComponent<AudioSource>();
    }
    public void PlayButtonClick()
    {
        audioM.PlayOneShot(buttonClick);
    }
    public void PlayCoinCollect()
    {
        audioM.PlayOneShot(coinCollect);
    }
    public void PlayJump()
    {
        audioM.PlayOneShot(jump);
    }
    public void PlayGameOver()
    {
        audioM.PlayOneShot(gameOver);
    }
    public void PlayHit()
    {
        audioM.PlayOneShot(hit);
    }
    public void PlayStartMusicBackground()
    {
        audioM.clip = StartMusicBackground;
        audioM.loop = true;
        audioM.Play();
    }
    public void PlayGameMusicBackground()
    {
        audioM.clip = GameMusicBackground;
        audioM.loop = true;
        audioM.Play();
    }
    public void PlayMonsterCollision()
    {
        audioM.PlayOneShot(monsterCollision);
    }

    public void PlayItemCollection()
    {
        audioM.PlayOneShot(itemCollection);
    }
    public void StopMusic()
    {
        audioM.Stop();
    }
}
