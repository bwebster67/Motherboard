using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerProjectile : MonoBehaviour
{
    // Reference to the pool so the bullet can return itself
    private ProjectilePool _pool; 
    private Transform _player;
    public ParticleSystem onHitEffect;
    public SpriteRenderer myRenderer; 
    public Collider2D myCollider; 
    private WeaponData _weaponData;
    protected float currentDamage;
    protected float currentKnockbackStrength;
    protected int currentPiercesLeft;

    public virtual void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Init(ProjectilePool pool, WeaponData weaponData)
    {
        _pool = pool;
        _weaponData = weaponData;
        currentDamage = weaponData.damage;
        currentPiercesLeft = weaponData.pierce;
        currentKnockbackStrength = weaponData.knockbackStrength;
    }
    protected virtual void Update()
    {
    }

    protected virtual void OnEnable()
    {
        Reset();
        
        // Auto-destroy (return to pool) after 10 seconds
        Invoke(nameof(Deactivate), 10f);
    }

    protected virtual void Deactivate()
    {
        // Cancel invoke to prevent errors if deactivated early
        StopAllCoroutines();
        CancelInvoke(nameof(Deactivate)); 

        
        // Return to pool
        _pool.ReturnProjectile(gameObject);
    }

    protected virtual void Reset()
    {
        transform.position = _player.transform.position;
        transform.rotation = Quaternion.identity;

        if (myRenderer != null) myRenderer.enabled = true;
        if (myCollider != null) myCollider.enabled = true;

        currentDamage = _weaponData.damage;
        currentPiercesLeft = _weaponData.pierce;
        currentKnockbackStrength = _weaponData.knockbackStrength;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Enemy"))
        {
            OnEnemyHit(collider2D);
        }
    }
    protected virtual void DamageEnemy(Enemy enemy)
    {
        enemy.TakeDamage(currentDamage);
        DamagePopupManager.SpawnDamagePopup(enemy.transform.position, currentDamage);
    }

    protected void ApplyKnockback(Enemy enemy)
    {
        Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
        enemy.rigidbody2D.AddForce(knockbackDirection * currentKnockbackStrength, ForceMode2D.Impulse);
    }

    protected virtual void OnEnemyHit(Collider2D collider2D)
    {
        Debug.Log($"Collided with {collider2D.name}");
        Enemy enemy = collider2D.GetComponent<Enemy>();
        enemy.Stun(0.25f);
        HandlePierce();
        ApplyKnockback(enemy);
        DamageEnemy(enemy);
    }

    protected virtual void HandlePierce()
    {
        if (currentPiercesLeft == 0)
        {
            Deactivate();
        }
        else
        {
            currentPiercesLeft -= 1;
        }
    }

    
}