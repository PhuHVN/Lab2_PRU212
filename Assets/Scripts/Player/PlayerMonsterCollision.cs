using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMonsterCollision : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TMP_Text scoreTextOnGameOver;
    private int currentScore = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("Game Over");
            //ScoreManager.Instance.AddScore(1);
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        //Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.deltaTime * 3);
        if(scoreTextOnGameOver != null)
        {
            currentScore =  ScoreManager.Instance.GetScore();
            scoreTextOnGameOver.text += currentScore.ToString();
        }

        if(gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        // Khi b?m nút Play Again, game s? ch?y l?i bình th??ng
        Time.timeScale = 1f;
        // T?i l?i Scene hi?n t?i
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
