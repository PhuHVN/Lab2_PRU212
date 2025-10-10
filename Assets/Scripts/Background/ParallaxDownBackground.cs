using UnityEngine;

public class ParallaxDownBackground : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY; 
    private float backgroundWidth;
    private float backgroundHeight;

    private Camera cam;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;
        backgroundHeight = sprite.texture.height / sprite.pixelsPerUnit;

        cam = Camera.main;
    }

    void Update()
    {
        transform.position += new Vector3(speedX, speedY, 0f) * Time.deltaTime;
    
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);
        if (screenPos.y < -1f || screenPos.x < -1f || screenPos.x > 2f)
        {
            transform.position += new Vector3(-Mathf.Sign(speedX) * backgroundWidth * 1.2f,
                                              -Mathf.Sign(speedY) * backgroundHeight * 2f, 0f);
        }
    }
}
