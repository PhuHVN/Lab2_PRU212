using UnityEngine;

public class PlayerHeartCollision : MonoBehaviour
{
    private PlayerMonsterCollision playerMonsterCollision;
    private HeartUIManager heartUIManager;

    void Awake()
    {
        // L?y các component trên cùng ??i t??ng Player khi game b?t ??u
        playerMonsterCollision = GetComponent<PlayerMonsterCollision>();

        // Tìm ??i t??ng HeartUIManager trong scene
        // Cách này an toàn h?n là ph?i kéo th? th? công
        heartUIManager = FindObjectOfType<HeartUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Ki?m tra xem có ph?i va ch?m v?i "Heart" item không
        if (other.CompareTag("Heart"))
        {
            // 2. Ki?m tra máu hi?n t?i (l?y t? script PlayerMonsterCollision)
            if (playerMonsterCollision.currentHealth < 3)
            {
                // 3. N?u máu ch?a ??y, t?ng máu lên 1
                playerMonsterCollision.currentHealth++;
                Debug.Log("H?i máu! Máu hi?n t?i: " + playerMonsterCollision.currentHealth);

                // 4. Yêu c?u HeartUIManager c?p nh?t l?i giao di?n
                if (heartUIManager != null)
                {
                    heartUIManager.UpdateHearts(playerMonsterCollision.currentHealth);
                }
            }
            else
            {
                Debug.Log("Máu ?ã ??y, không h?i thêm!");
            }

            // 5. Dù có h?i máu hay không, item v?n ph?i bi?n m?t
            //Destroy(other.gameObject);
            HandleItemCollection(other.gameObject);
        }
    }

    private void HandleItemCollection(GameObject item)
    {
        // 1. Vô hi?u hóa Collider ?? không trigger l?n n?a
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

        // 3. Phá h?y ??i t??ng sau m?t kho?ng tr? r?t nh?
        Destroy(item, 0.1f);
    }
}
