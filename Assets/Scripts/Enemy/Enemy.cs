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
    private float currentHealth;

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
        enemySpawnManager.activeEnemies.Remove(transform);
        ReturnToPool();
        Debug.Log("Enemy Died");
    }

    public void SetPool(IObjectPool<GameObject> pool) {
        _myPool = pool;
    }

    public void ReturnToPool() {
        if (_myPool != null) {
            _myPool.Release(this.gameObject);
        } else {
            Destroy(gameObject); 
        }
    }

}
