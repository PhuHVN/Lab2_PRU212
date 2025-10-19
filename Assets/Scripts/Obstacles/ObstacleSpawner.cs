//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    [Header("References")]
//    public SpriteShapeGenerator terrain;     // Kéo SpriteShapeGenerator t? Scene vào ?ây
//    public Transform player;                 // Player

//    [Header("Obstacle Settings")]
//    public GameObject[] obstaclePrefabs;     // Danh sách prefab v?t c?n (rock, snowman, ...)
//    public float spawnInterval = 10f;        // Kho?ng cách X gi?a các v?t c?n
//    public float spawnOffsetY = 0.2f;        // ?? cao so v?i m?t ??t

//    private float nextSpawnX;

//    private List<GameObject> spawnedObstacles = new List<GameObject>();

//    void Start()
//    {
//        if (terrain == null || player == null || obstaclePrefabs.Length == 0)
//        {
//            Debug.LogWarning("?? Missing references on ObstacleSpawner!");
//            enabled = false;
//            return;
//        }

//        // Kh?i t?o ?i?m spawn ??u tiên
//        nextSpawnX = player.position.x + 15f;
//    }

//    void Update()
//    {
//        if (player == null || terrain == null) return;

//        // Spawn v?t c?n khi player t?i g?n nextSpawnX
//        if (player.position.x >= nextSpawnX - 20f)
//        {
//            SpawnObstacle(nextSpawnX);
//            nextSpawnX += spawnInterval;
//        }

//        // Xoá obstacle quá xa camera (t?i ?u performance)
//        CleanupOldObstacles();
//    }

//    void SpawnObstacle(float x)
//    {
//        float groundY = terrain.GetGroundYAtX(x);
//        Vector3 spawnPos = new Vector3(x, groundY + spawnOffsetY, 0);

//        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
//        GameObject newObstacle = Instantiate(prefab, spawnPos, Quaternion.identity);
//        newObstacle.transform.parent = transform; // ?? d? qu?n lý trong Hierarchy

//        spawnedObstacles.Add(newObstacle);
//    }

//    void CleanupOldObstacles()
//    {
//        float camX = Camera.main.transform.position.x;

//        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
//        {
//            if (spawnedObstacles[i] == null) continue;

//            // N?u obstacle ?i xa kh?i camera thì xoá
//            if (spawnedObstacles[i].transform.position.x < camX - 30f)
//            {
//                Destroy(spawnedObstacles[i]);
//                spawnedObstacles.RemoveAt(i);
//            }
//        }
//    }
//}


using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    public SpriteShapeGenerator terrain;
    public Transform player;

    [Header("Obstacle Settings")]
    public List<ObstacleData> obstacles; // danh sách prefab + t? l?
    public float minSpawnInterval = 8f;  // kho?ng cách t?i thi?u
    public float maxSpawnInterval = 16f; // kho?ng cách t?i ?a
    public float yOffsetMin = 0f;
    public float yOffsetMax = 0.5f;

    //[Header("Spawn Delay")]
    //public float initialSpawnDelay = 2f;
    [Header("Delay Settings")]
    public float spawnDelayAfterStart = 2f;

    private float gameStartTime;
    private float nextSpawnX;
    //private bool canSpawn = false;
    private readonly List<GameObject> spawnedObstacles = new();

    void Start()
    {
        if (terrain == null || player == null || obstacles.Count == 0)
        {
            Debug.LogWarning("?? Missing references on ObstacleSpawner!");
            enabled = false;
            return;
        }

        gameStartTime = Time.time;
        //nextSpawnX = player.position.x + Random.Range(minSpawnInterval, maxSpawnInterval);
        nextSpawnX = float.MaxValue;
    }

    void Update()
    {
        if (Time.time - gameStartTime < spawnDelayAfterStart)
            return;

        if (player == null || terrain == null) return;

        // Sau delay -> kh?i t?o nextSpawnX 1 l?n
        if (nextSpawnX == float.MaxValue)
        {
            // spawn ??u tiên cách player m?t kho?ng xa h?n bình th??ng
            nextSpawnX = player.position.x + Random.Range(minSpawnInterval + 8f, maxSpawnInterval + 12f);
        }

        // Spawn khi player t?i g?n
        if (player.position.x >= nextSpawnX - 15f)
        {
            SpawnObstacle(nextSpawnX);
            nextSpawnX += Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        CleanupOldObstacles();
    }

    //public void StartSpawning()
    //{
    //    // G?i hàm này t? StartButton trong UI
    //    StartCoroutine(StartSpawningAfterDelay());
    //}

    //private System.Collections.IEnumerator StartSpawningAfterDelay()
    //{
    //    canSpawn = false;
    //    yield return new WaitForSeconds(initialSpawnDelay);
    //    canSpawn = true;
    //    Debug.Log("?? Obstacle spawning started!");
    //}

    void SpawnObstacle(float x)
    {
        //GameObject prefab = GetRandomObstacle();
        //if (prefab == null) return;

        //// Tính ?úng m?t ??t
        //float groundY = terrain.GetGroundYAtX(x);
        //float offsetY = Random.Range(yOffsetMin, yOffsetMax);
        //Vector3 spawnPos = new Vector3(x, groundY + offsetY, 0);

        //// Instantiate
        //GameObject newObstacle = Instantiate(prefab, spawnPos, Quaternion.identity);
        //newObstacle.transform.SetParent(transform);

        //var monster = newObstacle.GetComponent<MonsterMovement>();
        //if (monster != null)
        //{
        //    monster.SetTerrain(terrain);
        //}


        //spawnedObstacles.Add(newObstacle);

        GameObject prefab = GetRandomObstacle();
        if (prefab == null) return;

        float groundY = terrain.GetGroundYAtX(x);
        float offsetY = Random.Range(yOffsetMin, yOffsetMax);

        // ?? Ki?m tra xem object có ph?i là Monster không
        GameObject newObstacle = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        newObstacle.transform.SetParent(transform);

        // ?? N?u có MonsterMovement, ch?nh cao h?n m?t ??t
        var monster = newObstacle.GetComponent<MonsterMovement>();
        if (monster != null)
        {
            float monsterHeightOffset = 0.5f; // T?ng / gi?m tùy kích th??c monster
            newObstacle.transform.position = new Vector3(x, groundY + monsterHeightOffset, 0);
            monster.SetTerrain(terrain);
        }
        else
        {
            // Các obstacle bình th??ng
            newObstacle.transform.position = new Vector3(x, groundY + offsetY, 0);
        }

        spawnedObstacles.Add(newObstacle);
    }

    GameObject GetRandomObstacle()
    {
        float total = 0;
        foreach (var o in obstacles) total += o.spawnChance;

        float rand = Random.value * total;
        float sum = 0;

        foreach (var o in obstacles)
        {
            sum += o.spawnChance;
            if (rand <= sum)
                return o.prefab;
        }
        return obstacles[0].prefab; // fallback
    }

    void CleanupOldObstacles()
    {
        float camX = Camera.main.transform.position.x;

        for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
        {
            if (spawnedObstacles[i] == null)
            {
                spawnedObstacles.RemoveAt(i);
                continue;
            }

            if (spawnedObstacles[i].transform.position.x < camX - 40f)
            {
                Destroy(spawnedObstacles[i]);
                spawnedObstacles.RemoveAt(i);
            }
        }
    }

    public List<GameObject> GetSpawnedObstacles()
    {
        return spawnedObstacles;
    }
}

[System.Serializable]
public class ObstacleData
{
    public GameObject prefab;
    [Range(0f, 1f)] public float spawnChance = 0.3f;
}
