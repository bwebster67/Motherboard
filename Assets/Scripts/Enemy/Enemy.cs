using UnityEngine; 

public abstract class Enemy : MonoBehaviour, IDamageable 
{
    public EnemySO enemyData;
    public Rigidbody2D rigidbody2D;
    public GameObject player;

    // Stats
    public float currentHealth;

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
        if (currentHealth <= 0)
        {
            Die();
        }
        return currentHealth;
    }

    protected virtual void Die()
    {
    }
}
