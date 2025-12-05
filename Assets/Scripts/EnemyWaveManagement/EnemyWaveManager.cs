using UnityEngine;

public class EnemyWaveManager : MonoBehaviour {
    [Header("Configuration")]
    public LevelConfig currentLevel;
    public Transform playerTransform;
    public EnemySpawnManager enemySpawnManager;

    private int currentWaveIndex = 0;
    private float waveTimer;
    private float spawnTimer;

    void Awake()
    {
        if (enemySpawnManager == null) {enemySpawnManager = FindAnyObjectByType<EnemySpawnManager>(); }
    }

    void Start() {
        if(currentLevel == null || currentLevel.waves.Count == 0) {
            Debug.LogError("No Level Config assigned!");
            enabled = false;
        }
    }

    void Update() {
        HandleWaveLogic();
    }

    void HandleWaveLogic() {
        if (currentWaveIndex >= currentLevel.waves.Count) return; // Level Over

        WaveSegment currentWave = currentLevel.waves[currentWaveIndex];

        // 1. Handle Wave Duration
        waveTimer += Time.deltaTime;
        if (waveTimer >= currentWave.duration) {
            NextWave();
            return;
        }

        // 2. Handle Spawning Interval
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentWave.spawnRate) {
            SpawnEnemy(currentWave);
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy(WaveSegment wave) {
        string tag = wave.enemyTags[Random.Range(0, wave.enemyTags.Length)];
        Vector3 pos = GetRandomSpawnPosition();
        
        enemySpawnManager.Spawn(tag, pos, Quaternion.identity);
    }

    void NextWave() {
        currentWaveIndex++;
        waveTimer = 0f;
        spawnTimer = 0f;
        Debug.Log($"Wave {currentWaveIndex} Started");
    }
    public Vector3 GetRandomSpawnPosition() {
        // Determine a radius that is definitely outside the screen
        // You can cache 'camHeight' and 'camWidth' in Start()
        float screenAspect = (float)Screen.width / Screen.height;
        float camHeight = Camera.main.orthographicSize * 2;
        float camWidth = camHeight * screenAspect;
        
        // Add a buffer so they don't spawn exactly on the edge
        float spawnRadius = Mathf.Max(camWidth, camHeight) * 0.6f + 2f; 

        // Get a random point on a circle
        Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;
        
        // Return that point relative to the player
        return playerTransform.position + new Vector3(randomPoint.x, randomPoint.y, 0);
    }
}