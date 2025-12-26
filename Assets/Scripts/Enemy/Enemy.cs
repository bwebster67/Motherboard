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
    public float collisionDamageGraceTimeLeft = 0;

    // Pooling
    private IObjectPool<GameObject> _myPool;

    // Context
    EnemySpawnManager enemySpawnManager;

    // Stats
    public float currentHealth;

    [Header("White Flash")] 
    public SpriteRenderer spriteRenderer;
    public Material baseMaterial;
    public Material whiteFlashMaterial;
    public float flashDuration = 0.1f;

    protected virtual void Awake()
    {
        enemySpawnManager = FindAnyObjectByType<EnemySpawnManager>();
    }

    protected virtual void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        baseMaterial = spriteRenderer.material;

        currentHealth = enemyData.baseHealth;
    }

    protected virtual void Update()
    {
        if (!stunned) Move();
        if (collisionDamageGraceTimeLeft > 0) {collisionDamageGraceTimeLeft -= Time.deltaTime;}
    }

    public abstract void Move();

    // Contact Damage 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionDamageGraceTimeLeft > 0) {return;}

        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(enemyData.baseCollisionDamage);
            collisionDamageGraceTimeLeft = 0.25f;
        }
    }

    // Taking Damage and Stun
    public virtual float TakeDamage(float amount)
    {
        WhiteFlash();
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

    public void WhiteFlash()
    {
        StartCoroutine(WhiteFlashCoroutine());
    }

    public IEnumerator WhiteFlashCoroutine()
    {
        spriteRenderer.material = whiteFlashMaterial; 
        yield return new WaitForSeconds(flashDuration); 
        spriteRenderer.material = baseMaterial;
    }

    protected virtual void Die()
    {
        PlayerLevelManager.Instance.GainExp(enemyData.expValue);
        EnemyDeathEffectManager.SpawnEnemyDeathEffect(transform.position);
        ReturnToPool();
        Debug.Log("Enemy Died");
    }



    // Pooling
    public virtual void Reset()
    {
        enemySpawnManager.activeEnemies.Remove(transform);
        currentHealth = enemyData.baseHealth;
        stunned = false;
        collisionDamageGraceTimeLeft = 0;
        spriteRenderer.material = baseMaterial;
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
