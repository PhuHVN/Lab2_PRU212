using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f; // Player = 7f, n�n monster ch?m h?n
    public float destroyOffset = -30f;
    [Header("Terrain Alignment")]
    public float heightOffset = 0.5f;

    private Camera mainCam;
    private SpriteShapeGenerator terrain;

    void Start()
    {
        mainCam = Camera.main;
    }

    public void SetTerrain(SpriteShapeGenerator t)
    {
        terrain = t;
    }

    void Update()
    {
        // Di chuy?n sang tr�i theo th?i gian
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);

        // N?u c� terrain ? b�m theo m?t ??t
        if (terrain != null)
        {
            //float groundY = terrain.GetGroundYAtX(transform.position.x);
            //Vector3 pos = transform.position;
            //pos.y = groundY;
            //transform.position = pos;
            float groundY = terrain.GetGroundYAtX(transform.position.x);
            Vector3 pos = transform.position;
            pos.y = groundY + heightOffset;
            transform.position = pos;
        }

        // N?u ra kh?i t?m camera ? t? hu?
        if (mainCam && transform.position.x < mainCam.transform.position.x + destroyOffset)
        {
            Destroy(gameObject);
        }
    }
}
