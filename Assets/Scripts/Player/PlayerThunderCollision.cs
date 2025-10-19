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
            // H?y object Thunder ?? �?n� item
            //Destroy(other.gameObject);
            HandleItemCollection(other.gameObject);
            AudioMangement.instance.PlayItemCollection();

            // N?u ?ang c� buff th� reset l?i
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
        // 1. V� hi?u h�a Collider ?? kh�ng?? (trigger) l?n n?a
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 2. ?n v?t ph?m ?i (n?u c� Renderer)
        Renderer renderer = item.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // 3. Ph� h?y ??i t??ng sau m?t kho?ng tr? r?t nh? ?? ??m b?o c�c va ch?m kh�c ???c x? l�
        Destroy(item, 0.1f);
    }

    private IEnumerator ApplyThunderBuff()
    {
        // 1?? �p d?ng hi?u ?ng
        playerController.moveSpeed = originalMoveSpeed * speedBoostMultiplier;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }

        // 2?? Ch? trong th?i gian buff
        yield return new WaitForSeconds(thunderDuration);

        // 3?? Reset l?i t?c ?? v� m�u
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
        // N?u ?ang c� buff th� m?i h?y
        if (currentBuffCoroutine != null)
        {
            StopCoroutine(currentBuffCoroutine);
            ResetThunderBuff(); // Reset t?c ?? v� m�u s?c ngay l?p-t?c
            currentBuffCoroutine = null;

            // Y�u c?u UI c?ng ?n ?i
            thunderUI?.HideThunderBar();
        }
    }
}
