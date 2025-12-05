using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerProjectile : MonoBehaviour
{
    // Reference to the pool so the bullet can return itself
    private ProjectilePool _pool; 
    private Transform _player;
    private WeaponData _weaponData;
    private float currentDamage;
    int currentPiercesLeft;

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

        currentDamage = _weaponData.damage;
        currentPiercesLeft = _weaponData.pierce;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Enemy"))
        {
            collider2D.GetComponent<Enemy>().TakeDamage(currentDamage);
            Debug.Log($"Collided with {collider2D.name}");
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
}