using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;              // Camera (gán MainCamera ho?c Cinemachine camera)
    [SerializeField] private float parallaxEffect;    // 0.1f - 0.9f (nh? = xa)
    private float startPosX;
    private float length;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float dist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPosX + dist, transform.position.y, transform.position.z);

        // L?p background n?u camera di chuy?n quá xa
        if (temp > startPosX + length)
            startPosX += length;
        else if (temp < startPosX - length)
            startPosX -= length;
    }
}
