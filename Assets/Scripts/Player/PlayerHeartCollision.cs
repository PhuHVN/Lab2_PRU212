using UnityEngine;

public class PlayerHeartCollision : MonoBehaviour
{
    private PlayerMonsterCollision playerMonsterCollision;
    private HeartUIManager heartUIManager;

    void Awake()
    {
        // L?y c�c component tr�n c�ng ??i t??ng Player khi game b?t ??u
        playerMonsterCollision = GetComponent<PlayerMonsterCollision>();

        // T�m ??i t??ng HeartUIManager trong scene
        // C�ch n�y an to�n h?n l� ph?i k�o th? th? c�ng
        heartUIManager = FindObjectOfType<HeartUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Ki?m tra xem c� ph?i va ch?m v?i "Heart" item kh�ng
        if (other.CompareTag("Heart"))
        {
            // 2. Ki?m tra m�u hi?n t?i (l?y t? script PlayerMonsterCollision)
            if (playerMonsterCollision.currentHealth < 3)
            {
                // 3. N?u m�u ch?a ??y, t?ng m�u l�n 1
                playerMonsterCollision.currentHealth++;
                Debug.Log("H?i m�u! M�u hi?n t?i: " + playerMonsterCollision.currentHealth);

                // 4. Y�u c?u HeartUIManager c?p nh?t l?i giao di?n
                if (heartUIManager != null)
                {
                    heartUIManager.UpdateHearts(playerMonsterCollision.currentHealth);
                }
            }
            else
            {
                Debug.Log("M�u ?� ??y, kh�ng h?i th�m!");
            }

            // 5. D� c� h?i m�u hay kh�ng, item v?n ph?i bi?n m?t
            //Destroy(other.gameObject);
            HandleItemCollection(other.gameObject);
        }
    }

    private void HandleItemCollection(GameObject item)
    {
        // 1. V� hi?u h�a Collider ?? kh�ng trigger l?n n?a
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

        // 3. Ph� h?y ??i t??ng sau m?t kho?ng tr? r?t nh?
        Destroy(item, 0.1f);
    }
}
