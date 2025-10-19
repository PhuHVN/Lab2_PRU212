using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected: " + name);
            AudioMangement.instance.PlayCoinCollect();
            ScoreManager.Instance.AddScore(1);
            //Destroy(gameObject);

            Collider2D collider = GetComponent<Collider2D>();
            if(collider != null)
            {
                collider.enabled = false;
            }

            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
            Destroy(gameObject, 0.1f);
        }
    }

}
