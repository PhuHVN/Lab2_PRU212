using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float parallaxFactor; 

    private float backgroundWidth;
    private float startPosX;
    private float lastPlayerX;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;

        startPosX = transform.position.x;

    
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (player != null)
        {
            lastPlayerX = player.position.x;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

     
        float playerDeltaX = player.position.x - lastPlayerX;

        transform.position += new Vector3(playerDeltaX * parallaxFactor, 0f, 0f);

        
        lastPlayerX = player.position.x;


        float distanceFromPlayer = transform.position.x - player.position.x;

      
        if (distanceFromPlayer < -backgroundWidth)
        {
            transform.position += new Vector3(backgroundWidth * 2, 0f, 0f);
        }
        else if (distanceFromPlayer > backgroundWidth)
        {
            transform.position -= new Vector3(backgroundWidth * 2, 0f, 0f);
        }
    }
}