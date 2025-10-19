using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{ 
    [Header("References")]
    public SpriteShapeGenerator terrain;
    public Transform player;
    public ObstacleSpawner obstacleSpawner; 

    [Header("Item Settings")]
    public List<ItemData> items;
    public float minSpawnInterval = 6f;
    public float maxSpawnInterval = 14f;
    public float yOffsetMin = 1.0f;
    public float yOffsetMax = 2.0f;
    public float avoidObstacleRange = 4f;

    [Header("Delay Settings")]
    public float spawnDelayAfterStart = 2f;

    private float gameStartTime;
    private float nextSpawnX;
    private readonly List<GameObject> spawnedItems = new();

    void Start()
    {
        if (terrain == null || player == null || items.Count == 0)
        {
            Debug.LogWarning("?? Missing references on ItemSpawner!");
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

        if (nextSpawnX == float.MaxValue)
        {
            // spawn ??u tiên cách player m?t kho?ng xa h?n bình th??ng
            nextSpawnX = player.position.x + Random.Range(minSpawnInterval + 8f, maxSpawnInterval + 12f);
        }

        if (player.position.x >= nextSpawnX - 10f)
        {
            TrySpawnItem(nextSpawnX);
            nextSpawnX += Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        CleanupOldItems();
    }

    void TrySpawnItem(float x)
    {
        // tránh spawn n?u g?n obstacle
        if (obstacleSpawner != null)
        {
            foreach (var o in obstacleSpawner.GetSpawnedObstacles())
            {
                if (o == null) continue;

                if (Mathf.Abs(o.transform.position.x - x) < avoidObstacleRange)
                    return; // b? qua, quá g?n obstacle
            }
        }

        GameObject prefab = GetRandomItem();
        if (prefab == null) return;

        float groundY = terrain.GetGroundYAtX(x);
        float offsetY = Random.Range(yOffsetMin, yOffsetMax);
        Vector3 spawnPos = new Vector3(x, groundY + offsetY, 0);

        GameObject newItem = Instantiate(prefab, spawnPos, Quaternion.identity);
        newItem.transform.SetParent(transform);
        spawnedItems.Add(newItem);
    }

    GameObject GetRandomItem()
    {
        float total = 0;
        foreach (var i in items) total += i.spawnChance;

        float rand = Random.value * total;
        float sum = 0;

        foreach (var i in items)
        {
            sum += i.spawnChance;
            if (rand <= sum)
                return i.prefab;
        }
        return null;
    }

    void CleanupOldItems()
    {
        float camX = Camera.main.transform.position.x;
        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            if (spawnedItems[i] == null) continue;
            if (spawnedItems[i].transform.position.x < camX - 40f)
            {
                Destroy(spawnedItems[i]);
                spawnedItems.RemoveAt(i);
            }
        }
    }
}

[System.Serializable]
public class ItemData
{
    public GameObject prefab;
    [Range(0f, 1f)] public float spawnChance = 0.3f;
}
