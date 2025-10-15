using UnityEngine;
using UnityEngine.U2D;

public class ParallaxDownBackground : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    private float backgroundWidth;
    private float backgroundHeight;
    //private SpriteShapeGenerator terrain;


    private Camera cam;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;
        backgroundHeight = sprite.texture.height / sprite.pixelsPerUnit;
        //terrain = GetComponent<SpriteShapeGenerator>();
        cam = Camera.main;
    }

    void Update()
    {
        //transform.position += new Vector3(speedX, speedY, 0f) * Time.deltaTime;
        // 
        transform.position += new Vector3(speedX, speedY, 0f) * Time.deltaTime;
        Vector3 screenPos = cam.WorldToViewportPoint(transform.position);
        if (screenPos.y < -1f || screenPos.x < -1f || screenPos.x > 2f)
        {
            transform.position += new Vector3(-Mathf.Sign(speedX) * backgroundWidth * 1.2f,
                                              -Mathf.Sign(speedY) * backgroundHeight * 2f, 0f);
        }

        // L?y ?? cao m?t ??t t?i v? trí X hi?n t?i
        //if (terrain != null)
        //{
        //    float groundY = terrain.GetGroundYAtX(transform.position.x);

        //    // N?u tuy?t th?p h?n m?t ??t => d?ng l?i / reset
        //    if (transform.position.y <= groundY)
        //    {
        //        ResetSnowPositionAboveCamera();
        //        return;
        //    }
        //}
        //float camBottom = cam.transform.position.y - cam.orthographicSize - backgroundHeight / 2;
        //if (transform.position.y < camBottom)
        //{
        //    float camTop = cam.transform.position.y + cam.orthographicSize + backgroundHeight / 2;
        //    transform.position = new Vector3(transform.position.x, camTop, transform.position.z);
        //    //ResetSnowPositionAboveCamera();
        //}

    }

    void ResetSnowPositionAboveCamera()
    {
        Vector3 camPos = cam.transform.position;
        float randomX = camPos.x + Random.Range(-8f, 8f);
        float spawnY = camPos.y + 6f; // spawn l?i trên ??u màn hình

        transform.position = new Vector3(randomX, spawnY, transform.position.z);
    }

    //[SerializeField] private float speedX;
    //[SerializeField] private float speedY;
    //[SerializeField] private Transform otherSnow; // tuy?t còn l?i (?? bi?t v? trí reset)
    //private float backgroundWidth;
    //private float backgroundHeight;
    //private Camera cam;

    //void Start()
    //{
    //    Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    //    backgroundWidth = sprite.texture.width / sprite.pixelsPerUnit;
    //    backgroundHeight = sprite.texture.height / sprite.pixelsPerUnit;
    //    cam = Camera.main;
    //}

    //void Update()
    //{
    //    // Di chuy?n theo h??ng parallax (ngang + d?c)
    //    transform.position += new Vector3(speedX, speedY, 0f) * Time.deltaTime;

    //    // Khi tuy?t r?i xu?ng kh?i camera
    //    float camBottom = cam.transform.position.y - cam.orthographicSize - backgroundHeight / 2;
    //    if (transform.position.y < camBottom)
    //    {
    //        // Reset prefab này lên trên prefab còn l?i
    //        float newY = otherSnow.position.y + backgroundHeight;
    //        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    //    }

    //    // N?u di chuy?n ngang (speedX != 0), c?n reset l?i khi thoát kh?i camera trái/ph?i
    //    float camLeft = cam.transform.position.x - cam.orthographicSize * cam.aspect - backgroundWidth / 2;
    //    float camRight = cam.transform.position.x + cam.orthographicSize * cam.aspect + backgroundWidth / 2;
    //    if (transform.position.x < camLeft)
    //    {
    //        transform.position = new Vector3(otherSnow.position.x + backgroundWidth, transform.position.y, transform.position.z);
    //    }
    //    else if (transform.position.x > camRight)
    //    {
    //        transform.position = new Vector3(otherSnow.position.x - backgroundWidth, transform.position.y, transform.position.z);
    //    }
    //}


}
