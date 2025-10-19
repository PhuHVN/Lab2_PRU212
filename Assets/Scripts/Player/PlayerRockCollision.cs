using System.Collections;
using UnityEngine;

public class PlayerRockCollision : MonoBehaviour
{
    [Header("Debuff Settings")]
    // THAY ??I: Hai bi?n th?i gian ri�ng bi?t
    public float rockDuration = 1f; // Th?i gian debuff cho "Rock"
    public float rock2Duration = 3f; // Th?i gian debuff cho "Rock-2"

    public float speedDebuffMultiplier = 0.5f;
    public float jumpDebuffMultiplier = 0.5f;

    // Tham chi?u ??n c�c component kh�c tr�n c�ng m?t ??i t??ng Player
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    private PlayerThunderCollision playerThunderCollision;

    // Bi?n ?? l?u tr? c�c gi� tr? g?c
    private float originalMoveSpeed;
    private float originalJumpForce;
    private Color originalColor;

    // Bi?n ?? qu?n l� coroutine
    private Coroutine currentDebuffCoroutine;

    public bool isCheatActive = false;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerThunderCollision = GetComponent<PlayerThunderCollision>();
    }

    void Start()
    {
        if (playerController != null)
        {
            originalMoveSpeed = playerController.moveSpeed;
            originalJumpForce = playerController.jumpForce;
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // THAY ??I: C?p nh?t h�m va ch?m
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float durationToApply = 0f;

        // Ki?m tra xem va ch?m v?i tag n�o ?? l?y th?i gian debuff t??ng ?ng
        if (collision.gameObject.CompareTag("Rock"))
        {
            durationToApply = rockDuration;
        }
        else if (collision.gameObject.CompareTag("Rock-2"))
        {
            durationToApply = rock2Duration;
        }

        // N?u c� va ch?m v?i m?t trong c�c lo?i ?�
        if (durationToApply > 0f)
        {
            playerThunderCollision?.CancelThunderBuff();
            // D?ng coroutine c? n?u ?ang ch?y
            if (currentDebuffCoroutine != null)
            {
                StopCoroutine(currentDebuffCoroutine);
            }
            // B?t ??u coroutine m?i v� truy?n v�o th?i gian debuff
            currentDebuffCoroutine = StartCoroutine(ApplyDebuff(durationToApply));
        }
    }

    // THAY ??I: Coroutine gi? nh?n m?t tham s? 'duration'
    private IEnumerator ApplyDebuff(float duration)
    {
        if(isCheatActive)
        {
            yield break; 
        }
        // 1. �p d?ng hi?u ?ng
        playerController.moveSpeed = originalMoveSpeed * speedDebuffMultiplier;
        playerController.jumpForce = originalJumpForce * jumpDebuffMultiplier;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
        }

        // 2. Ch? theo th?i gian ???c truy?n v�o
        yield return new WaitForSeconds(duration);

        // 3. H?y hi?u ?ng
        ResetToOriginalStats();
        currentDebuffCoroutine = null;
    }

    private void ResetToOriginalStats()
    {
        if (playerController != null)
        {
            playerController.moveSpeed = originalMoveSpeed;
            playerController.jumpForce = originalJumpForce;
        }
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
