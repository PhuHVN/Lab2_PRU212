using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMonsterCollision : MonoBehaviour
{
    [Header("Health Settings")]
    private int maxHealth = 3;
    public int currentHealth;

    [Header("UI & Game Over")]
    public HeartUIManager heartUI;
    public GameObject heartsContainer;
    public GameObject gameOverCanvas;
    public TMP_Text scoreTextOnGameOver;
    private int currentScore = 0;

    [Header("Invincibility")]
    private bool isInvincible = false;
    public float invincibilityDuration = 1f;

    public bool isCheatActive = false;

    void Start()
    {
        // Kh?i t?o máu ban ??u
        currentHealth = maxHealth;
        if (heartUI != null)
        {
            heartUI.UpdateHearts(currentHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            //Debug.Log("Game Over");
            //ScoreManager.Instance.AddScore(1);
            //AudioMangement.instance.StopMusic();
            //AudioMangement.instance.PlayGameOver();
            if (!isInvincible)
            {
                TakeDamage(collision.gameObject);
            }
            //GameOver();
        }
    }

    void TakeDamage(GameObject monster)
    {
        if (!isCheatActive)
        {
            currentHealth--; // Gi?m 1 máu
        }


        // C?p nh?t l?i UI trái tim
        if (heartUI != null)
        {
            heartUI.UpdateHearts(currentHealth);
        }

        // Ki?m tra ?i?u ki?n thua
        //if (currentHealth <= 0)
        //{
        //    Debug.Log("Game Over");
        //    AudioMangement.instance.StopMusic();
        //    AudioMangement.instance.PlayGameOver();
        //    GameOver();
        //}
        //else
        //{
        //    // N?u ch?a thua, b?t ??u tr?ng thái b?t t? t?m th?i
        //    StartCoroutine(InvincibilityCoroutine());
        //}

        if (currentHealth > 0)
        {
            StartCoroutine(InvincibilityCoroutine());
            Destroy(monster);
        }
        else
        {
            Debug.Log("Game Over");
            AudioMangement.instance.StopMusic();
            AudioMangement.instance.PlayGameOver();
            GameOver();
        }
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        // Optional: Làm cho player nh?p nháy ?? báo hi?u
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite != null)
        {
            for (float i = 0; i < invincibilityDuration; i += 0.15f)
            {
                playerSprite.enabled = !playerSprite.enabled;
                yield return new WaitForSeconds(0.15f);
            }
            playerSprite.enabled = true; // ??m b?o player hi?n l?i
        }

        isInvincible = false;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        //Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.deltaTime * 3);
        if (heartsContainer != null)
        {
            heartsContainer.SetActive(false);
        }
        if (scoreTextOnGameOver != null)
        {
            currentScore = ScoreManager.Instance.GetScore();
            scoreTextOnGameOver.text += currentScore.ToString();
        }

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        // Khi b?m nút Play Again, game s? ch?y l?i bình th??ng
        Time.timeScale = 1f;
        // T?i l?i Scene hi?n t?i
        AudioMangement.instance.PlayGameMusicBackground();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
