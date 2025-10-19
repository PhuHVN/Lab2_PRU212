using System.Collections;
using UnityEngine;

public class PlayerThunderCollision : MonoBehaviour
{
    
    [Header("Thunder Buff Settings")]
    public float thunderDuration = 3f;        // th?i gian t?ng t?c
    public float speedBoostMultiplier = 1.5f; // t?ng 50% t?c ??


    [Header("UI Referrence")]
    public ThunderUIController thunderUI;

    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    private float originalMoveSpeed;
    private Color originalColor;
    private Coroutine currentBuffCoroutine;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (playerController != null)
        {
            originalMoveSpeed = playerController.moveSpeed;
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Thunder"))
        {
            // H?y object Thunder ?? “?n” item
            //Destroy(other.gameObject);
            HandleItemCollection(other.gameObject);
            AudioMangement.instance.PlayItemCollection();

            // N?u ?ang có buff thì reset l?i
            if (currentBuffCoroutine != null)
            {
                StopCoroutine(currentBuffCoroutine);
            }

            // B?t ??u buff m?i
            currentBuffCoroutine = StartCoroutine(ApplyThunderBuff());
            thunderUI?.ShowThunderBar(thunderDuration);
        }
    }

    private void HandleItemCollection(GameObject item)
    {
        // 1. Vô hi?u hóa Collider ?? không?? (trigger) l?n n?a
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 2. ?n v?t ph?m ?i (n?u có Renderer)
        Renderer renderer = item.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // 3. Phá h?y ??i t??ng sau m?t kho?ng tr? r?t nh? ?? ??m b?o các va ch?m khác ???c x? lý
        Destroy(item, 0.1f);
    }

    private IEnumerator ApplyThunderBuff()
    {
        // 1?? Áp d?ng hi?u ?ng
        playerController.moveSpeed = originalMoveSpeed * speedBoostMultiplier;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }

        // 2?? Ch? trong th?i gian buff
        yield return new WaitForSeconds(thunderDuration);

        // 3?? Reset l?i t?c ?? và màu
        ResetThunderBuff();
        currentBuffCoroutine = null;
    }

    private void ResetThunderBuff()
    {
        playerController.moveSpeed = originalMoveSpeed;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void CancelThunderBuff()
    {
        // N?u ?ang có buff thì m?i h?y
        if (currentBuffCoroutine != null)
        {
            StopCoroutine(currentBuffCoroutine);
            ResetThunderBuff(); // Reset t?c ?? và màu s?c ngay l?p-t?c
            currentBuffCoroutine = null;

            // Yêu c?u UI c?ng ?n ?i
            thunderUI?.HideThunderBar();
        }
    }
}
