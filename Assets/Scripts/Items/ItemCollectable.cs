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
            Destroy(gameObject);
        }
    }

}
