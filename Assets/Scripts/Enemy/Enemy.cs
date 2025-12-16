using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Pool;
using JetBrains.Annotations;
using System.Collections;

public abstract class Enemy : MonoBehaviour, IDamageable 
{
    public EnemySO enemyData;
    public Rigidbody2D rigidbody2D;
    public GameObject player;
    public bool stunned = false;

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
        if (!stunned) Move();
    }

    public abstract void Move();

    // Contact Damage 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(enemyData.baseCollisionDamage);
        }
    }

    // Taking Damage and Stun
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

    public void Stun(float stunDuration)
    {
        StopCoroutine(nameof(StunCoroutine));
        StartCoroutine(StunCoroutine(stunDuration));
    }

    private IEnumerator StunCoroutine(float stunDuration)
    {
        stunned = true;
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
    }

    protected virtual void Die()
    {
        PlayerLevelManager.Instance.GainExp(enemyData.expValue);
        ReturnToPool();
        Debug.Log("Enemy Died");
    }



    // Pooling
    public virtual void Reset()
    {
        enemySpawnManager.activeEnemies.Remove(transform);
        currentHealth = enemyData.baseHealth;
        stunned = false;
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
