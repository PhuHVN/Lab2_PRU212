using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class SpriteShapeGenerator : MonoBehaviour
{
    public Transform player;
    public float segmentWidth = 5f;
    public int visibleSegments = 14;
    public float heightScale = 2.5f;
    public float slopeSmoothness = 0.5f;
    public float noiseScale = 0.15f;
    public float playerSpawnHeight = 2f;

    [Header("Height Limits")]
    public float minTerrainHeight = -1f; 
    public float maxTerrainHeight = 3f; 

    private SpriteShapeController shape;
    private Spline spline;
    private bool initialized = false;
    private float nextGenerateX;
    private float lastY = 0f;
    private int segmentCount = 0;

    void Start()
    {
        shape = GetComponent<SpriteShapeController>();
        spline = shape.spline;
        spline.Clear();


        if (player != null)
        {
            var rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.simulated = false;
        }
    }

    void Update()
    {
        if (player == null) return;

        if (!initialized)
        {
            InitializeTerrain();
            initialized = true;
        }

        if (player.position.x > nextGenerateX)
        {
            GenerateNextSegment();
        }
    }

    void InitializeTerrain()
    {
        spline.Clear();
        float x = -2f;

     
        lastY = (minTerrainHeight + maxTerrainHeight) / 2f;

        for (int i = 0; i < visibleSegments; i++)
        {
            float y = GetRandomY(i);
            spline.InsertPointAt(i, new Vector3(x, y, 0));

            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
   
            spline.SetLeftTangent(i, new Vector3(-0.5f, 0, 0));
            spline.SetRightTangent(i, new Vector3(0.5f, 0, 0));

            lastY = y;
            x += segmentWidth;
        }

        nextGenerateX = x - segmentWidth * 2;
        shape.BakeCollider();

        StartCoroutine(SmoothSpawnPlayer());
    }

    IEnumerator SmoothSpawnPlayer()
    {
      
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        
        float spawnX = segmentWidth * 3; 
        float groundY = GetGroundYAtX(spawnX);


        player.position = new Vector3(spawnX, groundY + playerSpawnHeight, player.position.z);

       
        yield return null;


        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.linearVelocity = Vector2.zero; // Reset velocity
        }
    }

    float GetRandomY(int index)
    {
        float rand = Random.value;
        float baseY = lastY;

        if (rand < 0.3f)
        {
            // Flat segment
            baseY += Random.Range(-0.1f, 0.1f);
        }
        else if (rand < 0.7f)
        {
            // Gentle slope
            float target = lastY + Random.Range(-heightScale, heightScale) * slopeSmoothness;
            baseY = Mathf.Lerp(lastY, target, 0.5f);
        }
        else
        {
            // Stronger slope / hill
            float target = lastY + Random.Range(-heightScale * 1.5f, heightScale * 1.5f);
            baseY = Mathf.Lerp(lastY, target, 0.8f);
        }

        float noise = (Mathf.PerlinNoise(index * noiseScale, 0.5f) - 0.5f) * heightScale * 0.4f;
        baseY += noise;


        baseY = Mathf.Clamp(baseY, minTerrainHeight, maxTerrainHeight);

        return baseY;
    }

    void GenerateNextSegment()
    {
        int pointCount = spline.GetPointCount();
        Vector3 lastPoint = spline.GetPosition(pointCount - 1);

        float newX = lastPoint.x + segmentWidth;
        float newY = GetRandomY(segmentCount);
        spline.InsertPointAt(pointCount, new Vector3(newX, newY, 0));

        spline.SetTangentMode(pointCount, ShapeTangentMode.Continuous);

        spline.SetLeftTangent(pointCount, new Vector3(-0.5f, 0, 0));
        spline.SetRightTangent(pointCount, new Vector3(0.5f, 0, 0));

        lastY = newY;

        if (pointCount > visibleSegments)
        {
            spline.RemovePointAt(0);

            for (int i = 0; i < spline.GetPointCount(); i++)
                spline.SetPosition(i, spline.GetPosition(i) - new Vector3(segmentWidth, 0, 0));

            transform.position += new Vector3(segmentWidth, 0, 0);
        }

        segmentCount++;
        nextGenerateX += segmentWidth;
        shape.BakeCollider();
    }

    float GetGroundYAtX(float x)
    {
   
        float localX = x - transform.position.x;

        for (int i = 0; i < spline.GetPointCount() - 1; i++)
        {
            Vector3 p1 = spline.GetPosition(i);
            Vector3 p2 = spline.GetPosition(i + 1);

            if (localX >= p1.x && localX <= p2.x)
            {
                float t = Mathf.InverseLerp(p1.x, p2.x, localX);
                return Mathf.Lerp(p1.y, p2.y, t) + transform.position.y;
            }
        }

        return lastY + transform.position.y;
    }
}