using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    float backgroundWidth;
    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }


    void Update()
    {
        float movementX = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(movementX, 0f, 0f);
        if (transform.position.x <= -backgroundWidth)
        {
            transform.position = new Vector3(transform.position.x + backgroundWidth, transform.position.y, transform.position.z);
        }
    }
}
