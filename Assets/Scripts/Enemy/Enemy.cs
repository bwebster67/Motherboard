using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Pool;

public abstract class Enemy : MonoBehaviour, IDamageable 
{
    public EnemySO enemyData;
    public Rigidbody2D rigidbody2D;
    public GameObject player;

    // Pooling
    private IObjectPool<GameObject> _myPool;

    // Context
    EnemySpawnManager enemySpawnManager;

    // Stats
    public float currentHealth;

    protected virtual void Awake()
    {
        enemySpawnManager = FindAnyObjectByType<EnemySpawnManager>();
    }

    protected virtual void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();

        currentHealth = enemyData.baseHealth;
    }

    protected virtual void Update()
    {
    }
    
    public virtual float TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Enemy took {amount} damage and is now at {currentHealth} health.");
        if (currentHealth <= 0)
        {
            Die();
        }
        return currentHealth;
    }

    protected virtual void Die()
    {
        PlayerLevelManager.Instance.GainExp(enemyData.expValue);
        ReturnToPool();
        Debug.Log("Enemy Died");
    }

    public virtual void Reset()
    {
        enemySpawnManager.activeEnemies.Remove(transform);
        currentHealth = enemyData.baseHealth;
    }

    public void SetPool(IObjectPool<GameObject> pool) {
        _myPool = pool;
    }

    public void ReturnToPool() {
        Reset();
        if (_myPool != null) {
            _myPool.Release(this.gameObject);
        } else {
            Destroy(gameObject); 
        }
    }

}
