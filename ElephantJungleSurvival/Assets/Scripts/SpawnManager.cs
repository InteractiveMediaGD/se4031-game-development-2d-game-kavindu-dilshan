using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacle Spawning")]
    public GameObject obstaclePrefab;
    public Transform obstacleSpawnPoint;
    public float obstacleSpawnInterval = 2.5f;
    public float obstacleMinY = -2f;
    public float obstacleMaxY = 2f;
    private float obstacleTimer;

    [Header("Item (Health) Spawning")]
    public GameObject healthPackPrefab;
    public Transform itemSpawnPoint;
    public float healthSpawnInterval = 8f;
    public float itemMinY = -4f;
    public float itemMaxY = 4f;
    private float healthTimer;

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;
    public float enemySpawnInterval = 4.5f;
    public float enemyMinY = -3.5f;
    public float enemyMaxY = 3.5f;
    private float enemyTimer;

    [Header("Anti-Overlap Settings")]
    [Tooltip("Ensures objects don't spawn exactly on top of each other by forcing a small gap")]
    public float minTimeBetweenSpawns = 1.0f;

    void Start()
    {
        obstacleTimer = obstacleSpawnInterval;
        healthTimer = healthSpawnInterval + Random.Range(1f, 3f); 
        
        // Offset the enemy start time so an elephant and a tree don't spawn on the exact same frame globally
        enemyTimer = enemySpawnInterval + 2f; 
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        // --- Handle Obstacles ---
        obstacleTimer -= Time.deltaTime;
        if (obstacleTimer <= 0)
        {
            SpawnPrefab(obstaclePrefab, obstacleSpawnPoint, obstacleMinY, obstacleMaxY);
            obstacleTimer = obstacleSpawnInterval;
            PreventOverlap(); // Force other timers to wait a second!
        }

        // --- Handle Health Packs ---
        healthTimer -= Time.deltaTime;
        if (healthTimer <= 0)
        {
            SpawnPrefab(healthPackPrefab, itemSpawnPoint, itemMinY, itemMaxY);
            healthTimer = healthSpawnInterval;
            PreventOverlap();
        }

        // --- Handle Enemies ---
        enemyTimer -= Time.deltaTime;
        if (enemyTimer <= 0)
        {
            SpawnPrefab(enemyPrefab, enemySpawnPoint, enemyMinY, enemyMaxY);
            enemyTimer = enemySpawnInterval;
            PreventOverlap();
        }
    }

    // Helper method to push back any timer that is about to hit zero, guaranteeing a physical gap in the jungle!
    private void PreventOverlap() 
    {
        if (obstacleTimer < minTimeBetweenSpawns) obstacleTimer = minTimeBetweenSpawns;
        if (healthTimer < minTimeBetweenSpawns) healthTimer = minTimeBetweenSpawns;
        if (enemyTimer < minTimeBetweenSpawns) enemyTimer = minTimeBetweenSpawns;
    }

    // Helper method to keep code clean and prevent repeating the instantiation logic 3 times!
    private void SpawnPrefab(GameObject prefabToSpawn, Transform spawnPoint, float minY, float maxY)
    {
        if (prefabToSpawn == null || spawnPoint == null) return;

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, randomY, 0);
        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
